using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class HostelMessRuleController : Controller
    {
        private readonly ApplicationDbContext _db;
        public HostelMessRuleController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<HostelMessRule> objHostelMessRuleList = _db.HostelMessRules;
            return View(objHostelMessRuleList);
        }

        public IActionResult Details()
        {
            IEnumerable<HostelMessRule> objHostelMessRuleList = _db.HostelMessRules;
            return View(objHostelMessRuleList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(HostelMessRule obj)
        {
            //if (ModelState.IsValid)
            //{
                _db.HostelMessRules.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Rule created successfully!";
                return RedirectToAction("Index");
            
            return View(obj);
        }

        //GET
        [HttpGet]
        public IActionResult Edit()
        {
            var HostelMessRuleFromDb = _db.HostelMessRules.FirstOrDefault();
            

            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id == id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (HostelMessRuleFromDb == null)
            {
                return NotFound();
            }
            return View(HostelMessRuleFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(HostelMessRule obj)
        {
           
                _db.HostelMessRules.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Rule updated successfully!";
                return RedirectToAction("Index");
            
            return View(obj);
        }


        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var HostelMessRuleFromDb = _db.HostelMessRules.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id == id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (HostelMessRuleFromDb == null)
            {
                return NotFound();
            }
            return View(HostelMessRuleFromDb);
        }
        //POST
        [HttpPost, ActionName("DeletePOST")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.HostelMessRules.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.HostelMessRules.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Rule deleted successfully!";
            return RedirectToAction("Index");

        }


    }
}
