using Microsoft.AspNetCore.Mvc;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Identity;
using BulkyBookWeb.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BulkyBookWeb.Controllers
{
    public class UserDetailController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signinManager;
        public UserDetailController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signinManager)

        {
            _userManager = userManager;
            _signinManager = signinManager;
        }
        [HttpGet]
        public async Task<ActionResult> UserProfile()
        {
            ViewBag.Message = "Welcome to my demo!";

            HosteliteData hosteliteData = new HosteliteData();
            ApplicationDbContext applicationDbContext = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());
            if (_signinManager.IsSignedIn(User))
            {

                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                // var user=_userManager.Users.Where(u => u.Id == _userManager.GetUserId(HttpContext.User)).Select(u => u).FirstOrDefault();
                //var user=await _userManager.FindByIdAsync(userid);

                hosteliteData = applicationDbContext.HosteliteDatas.Include(hd => hd.User).Where(hd => hd.userid.Equals(user.Id)).Select(hd => hd).FirstOrDefault();

            }

            return View(hosteliteData);
        }
        [HttpPost]
        public ActionResult ProfileData(profile profile)
        {
            ViewBag.Message = "Welcome to my demo!";
            return View();
        }
    }
}
