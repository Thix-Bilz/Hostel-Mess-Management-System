using Microsoft.AspNetCore.Mvc;
using BulkyBookWeb.Models;
using BulkyBookWeb.Data;
namespace BulkyBookWeb.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ProfileController(ApplicationDbContext db)
        {
            db = _db;
        }
        public ActionResult ProfileData()
        {
            ViewBag.Message = "Welcome to my demo!";
            return View();
        } 
        public ActionResult HosteliteProfile()
        {
            return View("~/Views/Shared/_HosteliteProfile.cshtml");
        }
    }
}
