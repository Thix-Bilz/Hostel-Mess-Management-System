using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.Controllers
{
    public class messMenuController : Controller
    {
        private readonly ApplicationDbContext _db;
        public messMenuController(ApplicationDbContext db)
        {
            _db = db;
        }
        
        

        public IActionResult Index()
        {
            IEnumerable<messMenu> objmessMenuList = _db.Menu;
            return View(objmessMenuList);
        }

        public IActionResult Details()
        {
            IEnumerable<messMenu> objmessMenuList = _db.Menu;
            return View(objmessMenuList);
        }

        //GET
        [HttpGet]
        public IActionResult Create()
        {
            if (_db.Menu.Count() < 7)
                return View();
            else
                return PartialView("~/Views/Shared/_Model.cshtml");
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(messMenu obj)
        {
            //if (obj.Day == obj || obj.Day == obj.Lunch || obj.Day == obj.Dinner)
            //{
            //    ModelState.AddModelError("Day", "Day cannot exactly match meal");
            //}
            
                _db.Menu.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Menu created successfully!";
                return RedirectToAction("Index");
            
            return View(obj);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var messMenuFromDb = _db.Menu.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id == id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (messMenuFromDb == null)
            {
                return NotFound();
            }
            return View(messMenuFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(messMenu obj)
        {
            //if (obj.Day == obj.Breakfast || obj.Day == obj.Lunch || obj.Day == obj.Dinner)
            //{
            //    ModelState.AddModelError("Day", "Day cannot exactly match the meal");
            //}
            if (ModelState.IsValid)
            {
                _db.Menu.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Menu updated successfully!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var messMenuFromDb = _db.Menu.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id == id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (messMenuFromDb == null)
            {
                return NotFound();
            }
            return View(messMenuFromDb);
        }
        //POST
        [HttpPost, ActionName("DeletePOST")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Menu.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Menu.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully!";
            return RedirectToAction("Index");

        }
        [HttpPost]
        public IActionResult UpdateMessMenuConfigurationId(List<MessMenuConfiguration> messMenuConfigurations)
        {

            List<messMenu> messMenu = _db.Menu.ToList();
            messMenu.AsEnumerable().Where(mm =>mm.MessMenuCofigId!=0).Select(mm=>mm.MessMenuCofigId=null).ToList();
            messMenuConfigurations.AsEnumerable().Select(mc=>messMenu.AsEnumerable().Where(mm => mm.Day.Equals(mc.DayOfWeek)).Select(mm =>mm.MessMenuCofigId=mc.Id).ToList()).ToList();
            //messMenu.AsEnumerable().Select(mm => messMenuConfigurations.AsEnumerable().Where(mc => mc.DayOfWeek.Equals(mm.Day)).Select(mc =>mm.MessMenuCofigId = mc.Id).ToList()).ToList();
            _db.SaveChanges();
            return View("Views/Home/Index.cshtml");
        }

    }
}
