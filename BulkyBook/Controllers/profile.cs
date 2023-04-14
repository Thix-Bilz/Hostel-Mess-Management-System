using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class profile : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
