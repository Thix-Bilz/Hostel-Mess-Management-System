using Microsoft.AspNetCore.Mvc;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace BulkyBookWeb.Controllers
{
    [Authorize]
    public class RegisterationController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        public RegisterationController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult RegsiterUser()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegsiterUser(Registeration registeration)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = registeration.UserName, Email = registeration.UserName };
           
                var result = await userManager.CreateAsync(user, registeration.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent:false);
                    return RedirectToAction("Index","Home");
                }
                foreach(var e in result.Errors)
                {
                    ModelState.AddModelError("", e.Description);
                }
              
            }
            return View();
        }
    }
}
