using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Manage.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TrendyShop.Data;

namespace TrendyShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<DeletePersonalDataModel> _logger;
        private readonly DataContext dataContext;
        private readonly UsersContext usersContext;

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<DeletePersonalDataModel> logger,
            DataContext dataContext,
            UsersContext usersContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            this.dataContext = dataContext;
            this.usersContext = usersContext;

        }

        public IActionResult Index()
        {
            var users = usersContext.Users.ToList();
            return View(users);
        }

        public async Task<IActionResult> DeleteAccount(string userId)
        {
            var _user = usersContext.Users.Find(userId);
            var result = await _userManager.DeleteAsync(_user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred deleting user with ID '{userId}'.");
            }
            usersContext.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}