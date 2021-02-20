using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrendyShop.Data;
using TrendyShop.Models;
using TrendyShop.ViewModels;

namespace TrendyShop.Controllers
{
    [Authorize]
    public class LodgingController : Controller
    {
        private DataContext dataContext;
        private UserManager<IdentityUser> userManager;
        public LodgingController(DataContext ctx, UserManager<IdentityUser> _userManager)
        {
            dataContext = ctx;
            userManager = _userManager;
        }

        public async Task<IActionResult> Index(int? page, DateTime initialDate, DateTime currentSearchDate)
        {
            if (initialDate != default)
            {
                page = 1;
            }
            else
            {
                initialDate = currentSearchDate;
            }

            ViewData["CurrentSearchDate"] = initialDate;

            if (initialDate == default)
                initialDate = DateTime.Today;


            var purchasePerLodgins = dataContext.PurchasePerLodgings
                .Include(p => p.Lodging)
                .Include(p => p.GastronomicProduct)
                .Include(p => p.Lodging.Customer)
                .Include(p => p.Lodging.Employee)
                .AsEnumerable()
                .Where(l => l.Date >= initialDate)
                .GroupBy(p => p.Lodging)
                .ToList();

            var lodgings = dataContext.Lodgings.Include(l => l.Customer)
                .Include(l => l.Employee)
                //.Include(l => l.Incidences)
                .AsEnumerable()
                .Where(l => l.Date >= initialDate
                   && !l.Active
                   && !purchasePerLodgins.Exists(p => p.Key.Date == l.Date && p.Key.RoomId == l.RoomId)
                )
               .ToList();

            var lodgingsAndPurchases = new List<LodgingAndPurchase>();

            foreach (var p in purchasePerLodgins)
                lodgingsAndPurchases.Add(new LodgingAndPurchase { Purchase = p });

            foreach (var l in lodgings)
                lodgingsAndPurchases.Add(new LodgingAndPurchase { Lodging = l });

            lodgingsAndPurchases.Sort((l1, l2) => l1.Compare(l2));
            lodgingsAndPurchases.Reverse();

            int pageSize = 3;
            return View(await PaginatedList<LodgingAndPurchase>.CreateAsync(lodgingsAndPurchases, page ?? 1, pageSize));


        }


        public IActionResult Register(int roomId, DateTime _date)
        {
            RegisterViewModel rvm = new RegisterViewModel();
            var reservation = dataContext.Reservations.Single(r => r.RoomId == roomId && r.Date == _date);
            DateTime now = DateTime.Now;
            Lodging lodging = new Lodging
            {
                RoomId = roomId,
                Date = _date,
                FinalDate = reservation.FinalDate,
                Customer = new Customer
                {
                    Name = reservation.Customer,
                    Phone = reservation.Phone
                },
                RentCost = 0,//calculara automáticamente el precio estandar por hora de reservacion
                             //TotalPrice = 0,
                Companion = 1,
                Romantic = reservation.Romantic,
                LodgingNumber = dataContext.Lodgings.Count() + 1,
            };

            rvm.Lodging = lodging;

            Dictionary<string, List<LodgingIncidence>> clientIncidence = new Dictionary<string, List<LodgingIncidence>>();

            foreach (var customer in dataContext.Customers.ToArray())
            {
                List<Incidence> incidences = new List<Incidence>();

                var blockedDate = customer.LastBlockedDate;
                var CustomerIncidences = dataContext.LodgingIncidences.Include(l => l.Incidence).Where(l => (l.Lodging.Date.Day == blockedDate.Day && l.Lodging.Date.Month == blockedDate.Month
                                                                          && l.Lodging.Date.Year == blockedDate.Year) && l.Lodging.CustomerId == customer.CustomerId).ToList();

                clientIncidence.Add(customer.CustomerId, CustomerIncidences);
            }

            rvm.clientIncidences = clientIncidence;
            rvm.blockedCustomers = clientIncidence.Keys.ToArray();

            return View(rvm);

        }

        public IActionResult Details(int roomId, DateTime _date)
        {
            var vm = new LodgingDetailsViewModel
            {
                Lodging = dataContext.Lodgings.Include(l => l.Employee).Include(l => l.Customer).SingleOrDefault(l => l.RoomId == roomId && l.Date == _date),
                Purchase = dataContext.PurchasePerLodgings.Include(p => p.GastronomicProduct).Where(p => p.Lodging.RoomId == roomId && p.Lodging.Date == _date).ToList(),
                Incidences = dataContext.LodgingIncidences.Include(i => i.Incidence).Where(i => i.Lodging.RoomId == roomId && i.Lodging.Date == _date).ToList()
            };
            return View(vm);
        }

        private float StimateRentCost( DateTime initialDate, DateTime finalDate, bool romantic, int nPeople, out bool doubleReservation)
        {

            float rentCost = dataContext.SystemDefaultPrices.Single(s => s.Name == "RentCost").Amount;
            float romanticTax = dataContext.SystemDefaultPrices.Single(s => s.Name == "RomanticTax").Amount;
            float lateHoursTax = dataContext.SystemDefaultPrices.Single(s => s.Name == "LateHoursTax").Amount;
            float companionTax = dataContext.SystemDefaultPrices.Single(s => s.Name == "CompanionTax").Amount;

            Tuple<int, int>[] turns = { new Tuple<int, int>(0, 8),
                                         new Tuple<int, int>(8, 12),
                                         new Tuple<int, int>(12, 15),
                                         new Tuple<int, int>(15, 18),
                                         new Tuple<int, int>(18, 22),
                                         new Tuple<int, int>(22, 24)};
            var numTurns = 0;
            float stimateRentCost = 0;
            int start = 0;
            var foundFinalTurn = false;
            bool[] turnsOccupied = new bool[6];

            if (finalDate == new DateTime(finalDate.Year, finalDate.Month, finalDate.Day, 0, 0, 0))
                finalDate = new DateTime(finalDate.Year, finalDate.Month, finalDate.Day, 23, 59, 0);

            for (int i = 0; i < turns.Length; i++)
            {
                if ((initialDate.Hour + initialDate.Minute * 0.01) >= turns[i].Item1 && (initialDate.Hour + initialDate.Minute * 0.01) < turns[i].Item2)
                {
                    start = i;
                    turnsOccupied[i] = true;
                    break;
                }
            }

            int index = start - 1;
            while (!foundFinalTurn)
            {
                numTurns += 1;
                index = (index + 1) % 6;
                turnsOccupied[index] = true;
                if ((finalDate.Hour + finalDate.Minute * 0.01) > turns[index].Item1 && (finalDate.Hour + finalDate.Minute * 0.01) <= turns[index].Item2)
                    foundFinalTurn = true;
            }

            if (romantic)
                stimateRentCost += romanticTax;
            if (nPeople > 2)
                stimateRentCost += companionTax * (nPeople - 2);//aqui se asume que ya se valido que fueran menos de 4 personas

            if (!turnsOccupied[0] && !turnsOccupied[turnsOccupied.Length - 1])
            {
                doubleReservation = false;
                stimateRentCost += numTurns * rentCost;
                return stimateRentCost;
            }
            else if (turnsOccupied[0] && turnsOccupied[turnsOccupied.Length - 1])
            {
                numTurns -= 1;//aqui se podria decir que la reserva es doble
            }
            doubleReservation = true;
            if (numTurns == 1)
                stimateRentCost += lateHoursTax;
            else
                stimateRentCost += (numTurns - 1) * rentCost + lateHoursTax;

            return stimateRentCost;
        }


        public IActionResult Save(RegisterViewModel rvm)
        {

            var reservation = dataContext.Reservations.Find(rvm.Lodging.RoomId, rvm.Lodging.Date);
            reservation.IsActive = true;

            var customer = dataContext.Customers.Find(rvm.Lodging.Customer.CustomerId);
            if (customer == null)
            {
                rvm.Lodging.Customer.LastEntrance = DateTime.Today;
                dataContext.Customers.Add(rvm.Lodging.Customer);
                dataContext.SaveChanges();
            }
            else
            {
                customer.LastEntrance = DateTime.Today;
                customer.Phone = rvm.Lodging.Customer.Phone;
                rvm.Lodging.Customer = customer;
                dataContext.SaveChanges();
            }


            rvm.Lodging.CustomerId = rvm.Lodging.Customer.CustomerId;
            rvm.Lodging.Active = true;
            dataContext.Lodgings.Add(rvm.Lodging);
            dataContext.SaveChanges();
            return RedirectToAction("Index", "Home");
        }


        public IActionResult FinishLodging(/*int roomId, DateTime date*/PurchaseViewModel pvm)
        {
            var lodgingToClose = dataContext.Lodgings.Include(l => l.Customer)
               .Single(l => (l.RoomId == pvm.RoomId && l.Date == pvm.InitialDate));
            lodgingToClose.Consumption = 0;
            var purchase = new List<PurchasePerLodging>();
            if (pvm.RoomProducts != null)
            {
                for (int i = 0; i < pvm.RoomProducts.Count; i++)
                {
                    if (pvm.AmountToMove[i] != 0)
                    {
                        purchase.Add(new PurchasePerLodging
                        {
                            Cost = pvm.RoomProducts[i].Cost,
                            Name = pvm.RoomProducts[i].Name,
                            GastronomicProduct = dataContext.GastronomicProducts.Find(pvm.RoomProducts[i].Cost, pvm.RoomProducts[i].Name),
                            //RoomId = pvm.RoomId,//puede que esto y fecha sean prescindibles aqui
                            //Date = pvm.InitialDate,//controlar que la fecha que se pase sea la correcta
                            Amount = pvm.AmountToMove[i]
                        });
                    }
                }
            }
            var incidences = dataContext.Incidences;
            int greatestId = 0;
            if (incidences.Count() > 0)
            {
                greatestId = incidences.OrderByDescending(inc => inc.IncidenceId).First().IncidenceId;
            }

            DateTime now = DateTime.Now;
            var clvm = new CloseLodgingViewModel
            {
                Lodging = lodgingToClose,
                Employees = dataContext.Employees.ToList(),
                //Purchase = dataContext.PurchasePerLodgings.Where(pl => pl.RoomId == pvm.RoomId && pl.Date == pvm.InitialDate),
                Purchase = purchase,
                Incidences = dataContext.Incidences.ToList(),
                IncidencesToBind = new List<Incidence>(),
                SelectedIncidence = new Incidence(),
                OldIncidence = true,
                TotalPrice = 0,//2:9,
                GreatestId = greatestId,
                DateFormat = now.ToString("yyyy-MM-dd") + "T" + now.ToString("HH:mm")

                //IncidencesToBind = new List<Incidence>()
            };

            bool doubleR = false;
            clvm.Lodging.RentCost = StimateRentCost(lodgingToClose.Date, lodgingToClose.FinalDate, lodgingToClose.Romantic, lodgingToClose.Companion, out doubleR);
            clvm.Lodging.IsDouble = doubleR;
            clvm.Lodging.ActualFDate = now;
            //return View("CloseLodgingForm", clvm);
            return View(clvm);
        }

        [HttpPost]
        public IActionResult SaveLodgingRecords(CloseLodgingViewModel clvm)
        {
            var lodgingToClose = dataContext.Lodgings.Include(l => l.Customer)
                  //.Include(l => l.Reservation)
                  .Single(l => (l.RoomId == clvm.Lodging.RoomId && l.Date == clvm.Lodging.Date));

            if (!ModelState.IsValid)//extra charge y final price
            {
                DateTime now = DateTime.Now;
                var newClvm = new CloseLodgingViewModel//verificar que pasa con la tabla si da invalid model
                {
                    Lodging = lodgingToClose,
                    Employees = dataContext.Employees.ToList(),//arreglar puchase si no es valido
                    Purchase = dataContext.PurchasePerLodgings.Where(pl => pl.RoomId == clvm.Lodging.RoomId && pl.Date == clvm.Lodging.Date).ToList(),
                    Incidences = dataContext.Incidences.ToList(),
                    IncidencesToBind = clvm.IncidencesToBind,//podemos quedarnos solo con los int= id y cargar los incidesnces reales en cada iteracion
                    SelectedIncidence = new Incidence(),
                    TotalPrice = 0,
                    GreatestId = clvm.GreatestId,
                    DateFormat = now.ToString("yyyy-MM-dd") + "T" + now.ToString("HH:mm")
                };

                newClvm.Lodging.IsDouble = clvm.Lodging.IsDouble;
                newClvm.Lodging.RentCost = clvm.Lodging.RentCost;
                newClvm.Lodging.ActualFDate = DateTime.Now;
                return View("FinishLodging", newClvm);
            }//insertar purcahse de este lodging aqui al final

            //lodgingToClose.Reservation.FinishedOrCancelled = true;
            lodgingToClose.IsDouble = clvm.Lodging.IsDouble;
            lodgingToClose.RentCost = clvm.Lodging.RentCost;
            lodgingToClose.Consumption = clvm.Lodging.Consumption;
            lodgingToClose.ActualFDate = clvm.FinalDate;
            lodgingToClose.Active = false;
            lodgingToClose.ExtraCharge = clvm.Lodging.ExtraCharge;
            //lodgingToClose.TotalPrice = clvm.Lodging.TotalPrice;//2:9



           

            //// Busco todos los clientes bloquedos
            var blockedCustomers = dataContext.Customers.Where(c => c.IsBlocked == true);
            lodgingToClose.Customer.IsBlocked = clvm.Lodging.Customer.IsBlocked;

            if (lodgingToClose.Customer.IsBlocked)
            {
                if (!(blockedCustomers.Any(c => c.CustomerId == lodgingToClose.CustomerId)))
                {
                    lodgingToClose.Customer.LastBlockedDate = DateTime.Today;
                }


            }

            string employeeId = userManager.GetUserId(User);
            lodgingToClose.EmployeeId = employeeId;
            lodgingToClose.Employee = dataContext.Employees.Find(employeeId);
            //incidencias

            var lodgingReservation = dataContext.Reservations.Find(clvm.Lodging.RoomId, clvm.Lodging.Date);
            dataContext.Reservations.Remove(lodgingReservation);

            dataContext.SaveChanges();

            if (clvm.IncidencesToBind != null)
            {
                for (int i = 0; i < clvm.IncidencesToBind.Count(); i++)
                {
                    var incidenceInDatabase = dataContext.Incidences.
                        SingleOrDefault(inc => inc.IncidenceId == clvm.IncidencesToBind[i].IncidenceId);

                    var incidenceId = clvm.IncidencesToBind[i].IncidenceId;
                    if (incidenceInDatabase == null)
                    {
                        var incidence = new Incidence
                        {
                            Subject = clvm.IncidencesToBind[i].Subject,
                            Description = clvm.IncidencesToBind[i].Description,
                            Price = clvm.IncidencesToBind[i].Price
                        };
                        dataContext.Incidences.Add(incidence);
                        dataContext.SaveChanges();

                        incidenceId = dataContext.Incidences.
                            Single(inc => inc.Subject == clvm.IncidencesToBind[i].Subject &&
                            inc.Price == clvm.IncidencesToBind[i].Price &&
                            inc.Description == clvm.IncidencesToBind[i].Description).IncidenceId;
                    }

                    var newLodgingIncidence = new LodgingIncidence
                    {
                        Date = clvm.Lodging.Date,
                        RoomId = clvm.Lodging.RoomId,
                        IncidenceId = incidenceId,
                    };
                    dataContext.LodgingIncidences.Add(newLodgingIncidence);
                    dataContext.SaveChanges();
                }
            }

            if (clvm.Purchase != null)
            {
                foreach (var purchasepl in clvm.Purchase)//si llega null explota
                {
                    var newppl = new PurchasePerLodging
                    {
                        Cost = purchasepl.Cost,
                        Name = purchasepl.Name,
                        Amount = purchasepl.Amount,
                        RoomId = lodgingToClose.RoomId,
                        Date = lodgingToClose.Date
                    };

                    dataContext.PurchasePerLodgings.Add(newppl);
                    dataContext.SaveChanges();
                }
            }
            return RedirectToAction("Index", "Home");//redirigir a update storage
        }

        [HttpPost]
        public IActionResult SearchForDate(Search searchForm)
        {
            return RedirectToAction("Index", searchForm);
        }
    }
}
