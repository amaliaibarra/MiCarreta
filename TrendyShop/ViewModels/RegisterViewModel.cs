using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrendyShop.Models;

namespace TrendyShop.ViewModels
{
    public class RegisterViewModel
    {
        public Lodging Lodging { get; set; }

        public string[] blockedCustomers { get; set; }
        public Dictionary<string, List<LodgingIncidence>> clientIncidences { get; set; }


    }
}

