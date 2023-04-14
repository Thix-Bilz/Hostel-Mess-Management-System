using Microsoft.AspNetCore.Mvc;
using BulkyBookWeb.Models;
using BulkyBookWeb.Data;
using Newtonsoft.Json;
namespace BulkyBookWeb.Controllers
{
    public class ConfigurationController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ConfigurationController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ConfigureMessMenuPrices(MessMenuPrices messMenuPrices)
        {
            return View();
        }
        [HttpGet]
        public IActionResult AdminConfiguration()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ConfigureMessMenu(List<MessMenuConfiguration> messMenuConfigurations)
        {
            return View();
        }
        [HttpGet]
        public JsonResult ChangeDayOfWeek()
        {
            List<MessMenuConfiguration> messMenuConfigurations = _db.messMenuConfigurations.ToList();
            foreach(var item in messMenuConfigurations)
            {
                if(item.Day=="Current Day")
                {
                    item.DayOfWeek = DateTime.Now.DayOfWeek;
                }
               else if (item.Day == "Next Day")
                {
                    item.DayOfWeek = DateTime.Now.AddDays(1).DayOfWeek;
                }
                else if (item.Day == "Next To Next Day")
                {
                    item.DayOfWeek = DateTime.Now.AddDays(2).DayOfWeek;
                }
            } 
                _db.SaveChanges();


            return Json(_db.messMenuConfigurations.ToList());
        }
    }
}
