using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class NamazTimingController : Controller
    {
        private readonly ApplicationDbContext _db;
        public NamazTimingController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<NamazTiming> objNamazTimingList = _db.NamazTimings;
            return View(objNamazTimingList);
        }

        public IActionResult Details()
        {
            IEnumerable<NamazTiming> objNamazTimingList = _db.NamazTimings;
            return View(objNamazTimingList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NamazTiming obj)
        {
            //if (obj.Namaz == obj.Namaztiming.ToString())
            //{
            //    ModelState.AddModelError("Namaz", "Namaz cannot exactly match the timing");
            //}
            if (ModelState.IsValid)
            {
                _db.NamazTimings.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Namaz timing created successfully!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var NamazTimingFromDb = _db.NamazTimings.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id == id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (NamazTimingFromDb == null)
            {
                return NotFound();
            }
            return View(NamazTimingFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(NamazTiming obj)
        {
            if (obj.Namaz == obj.Namaztiming.ToString())
            {
                ModelState.AddModelError("Namaz", "Namaz cannot exactly match the timing");
            }
            if (ModelState.IsValid)
            {
                _db.NamazTimings.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Namaz timing updated successfully!";
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
            var NamazTimingFromDb = _db.NamazTimings.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id == id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (NamazTimingFromDb == null)
            {
                return NotFound();
            }
            return View(NamazTimingFromDb);
        }
        //POST
        [HttpPost, ActionName("DeletePOST")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.NamazTimings.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.NamazTimings.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Namaz timing deleted successfully!";
            return RedirectToAction("Index");

        }


    }
}
