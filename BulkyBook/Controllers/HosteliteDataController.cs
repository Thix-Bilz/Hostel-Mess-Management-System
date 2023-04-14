using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text.Encodings;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Drawing;
using Syncfusion.Pdf.Grid;
using System.IO;
using System.Data;

namespace BulkyBookWeb.Controllers
{
    public class HosteliteDataController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _db;
        public HosteliteDataController(UserManager<IdentityUser> usermanager,ApplicationDbContext db)
        {
            _userManager = usermanager;
            _db = db;
          
        }
        public IActionResult Index()
        {
            IEnumerable<HosteliteData> objHosteliteDataList = _db.HosteliteDatas;
            return View(objHosteliteDataList);
        }

        public IActionResult Details()
        {
            IEnumerable<HosteliteData> objHosteliteDataList = _db.HosteliteDatas;
            return View(objHosteliteDataList);
        }

        //GET
        [HttpGet]
        public IActionResult Create()
        {
            string id=_userManager.GetUserId(User);
            HosteliteData hosteliteData = new HosteliteData();
            hosteliteData = _db.HosteliteDatas.Where(hd => hd.userid.Equals(id)).FirstOrDefault();
            return View(hosteliteData);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(HosteliteData obj)
        {
            var options = new DbContextOptions<ApplicationDbContext>();
            ApplicationDbContext dbContext = new ApplicationDbContext(options);
            //if (obj.Name == obj.RoomNumber.ToString())
            //{
            //    ModelState.AddModelError("Name", "The Display Order cannot exactly match Name");
            //}
            //var crypt = new SHA256Managed();
            //string hash = string.Empty;
            //byte[] salt = new byte[128 / 8];
            //using (var rngCSP=new RNGCryptoServiceProvider())
            //{
            //    rngCSP.GetNonZeroBytes(salt);
            //}
            //string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(obj.Password, salt, KeyDerivationPrf.HMACSHA512, 100000, 256 / 8));
            PasswordHasher<IdentityUser> passwordHasher = new PasswordHasher<IdentityUser>();
            var user = _userManager.Users.Where(u => u.Email.Equals(obj.User.Email)).Select(u => u).FirstOrDefault();
            //bool IsValidPassword=passwordHasher.VerifyHashedPassword(obj.User, user.PasswordHash, obj.User.PasswordHash).Equals(PasswordVerificationResult.Success);

                if (user!=null)
                {
                obj.userid = user.Id;
                obj.User = null;
                //dbContext.Entry(obj.User).State = EntityState.Unchanged;
                dbContext.HosteliteDatas.Add(obj);
                
                dbContext.SaveChanges();
                    TempData["success"] = "Hostelite created successfully!";
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
            var HosteliteDataFromDb = _db.HosteliteDatas.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id == id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (HosteliteDataFromDb == null)
            {
                return NotFound();
            }
            return View(HosteliteDataFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(HosteliteData obj)
        {
            if (obj.Name == obj.RoomNumber.ToString())
            {
                ModelState.AddModelError("Name", "Room number cannot exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                _db.HosteliteDatas.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Hostelite Data updated successfully!";
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
            var HosteliteDataFromDb = _db.HosteliteDatas.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id == id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (HosteliteDataFromDb == null)
            {
                return NotFound();
            }
            return View(HosteliteDataFromDb);
        }
        //POST
        [HttpPost, ActionName("DeletePOST")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.HosteliteDatas.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.HosteliteDatas.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Hostelite Date deleted successfully!";
            return RedirectToAction("Index");

        }
        

    }
}
