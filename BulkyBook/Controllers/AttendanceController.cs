using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace BulkyBookWeb.Controllers
{
    
    public class AttendanceController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _db;
        public AttendanceController(ApplicationDbContext db,UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }
        public IActionResult Index()
        {
           
            IEnumerable<Attendance> objAttendanceList = _db.Attendances.Where(a=>a.CreatedDateTime.Month==DateTime.Now.Month);
            return View(objAttendanceList);
        }

        public IActionResult Details()
        {
            IEnumerable<Attendance> objAttendanceList =_db.Attendances.Where(a=>a.CreatedDateTime.Date==DateTime.Now.Date).Select(a=>a);
            return View(objAttendanceList);
        }
        //GET
        [HttpGet]
        public IActionResult Create()
        {
            Attendance attendance=new Attendance();
            attendance.AttendanceDetails = new List<AttendanceDetails>();
            List<messMenu> messMenus = new List<messMenu>();
            List<MessMenuConfiguration> messMenuConfigurations = _db.messMenuConfigurations.Where(mc => mc.Breakfast.Equals(BreakFast.Yes) || mc.Lunch.Equals(Lunch.Yes) || mc.Dinner.Equals(Dinner.Yes)).Select(mc => mc).ToList();
            messMenus.AddRange(messMenuConfigurations.Select(mc => _db.Menu.Where(mm => mm.MessMenuCofigId.Equals(mc.Id)).FirstOrDefault()));
            attendance.AttendanceDetails=messMenuConfigurations.Where(mc => mc.Breakfast.Equals(BreakFast.Yes) || mc.Lunch.Equals(Lunch.Yes) || mc.Dinner.Equals(Dinner.Yes)).Select(mc =>messMenus.Where(mm => mm.MessMenuCofigId.Equals(mc.Id)).Select(mm =>new AttendanceDetails() { messMenuId = mm.MessMenuId,HostelitemessMenu = new messMenu() { Day = mc.DayOfWeek == mm.Day ? mm.Day : DayOfWeek.Sunday, Breakfast = mc.Breakfast.Equals(BreakFast.Yes) ? mm.Breakfast : null, Lunch = mc.Lunch.Equals(Lunch.Yes) ? mm.Lunch : null, Dinner = mc.Dinner.Equals(Dinner.Yes) ? mm.Dinner : null } }).FirstOrDefault()).ToList();
            return View(attendance);
        }
        //attendance.AttendanceDetails.AddRange(new List<AttendanceDetails>() { new AttendanceDetails() { messMenuId =1,HostelitemessMenu = new messMenu() { Day=messMenus.Where(mm=>messMenuConfigurations.FirstOrDefault().DayOfWeek.Equals(mm.Day)).FirstOrDefault().Day,Lunch=messMenuConfigurations.Where(mc=>mc.Lunch.Equals("Yes")).Select(mc=>messMenus.Where(mm=>mm.MessMenuCofigId.Equals(mc.Id)).Select(mm=>mm).FirstOrDefault()).FirstOrDefault().Lunch, Dinner = messMenus.AsEnumerable().Where(m => m.Day.Equals(DateTime.Now.DayOfWeek)).Select(m => m).FirstOrDefault().Dinner } }, new AttendanceDetails() { messMenuId = messMenus.AsEnumerable().Where(m => m.Day.Equals(DateTime.Now.AddDays(1).DayOfWeek)).Select(m => m).FirstOrDefault().MessMenuId, HostelitemessMenu = new messMenu() { Day = messMenus.AsEnumerable().Where(m => m.Day.Equals(DateTime.Now.AddDays(1).DayOfWeek)).Select(m => m).FirstOrDefault().Day, Breakfast = messMenus.AsEnumerable().Where(m => m.Day.Equals(DateTime.Now.AddDays(1).DayOfWeek)).Select(m => m).FirstOrDefault().Breakfast } }
        //    });
        //messMenuConfigurations.AsEnumerable().Where(mc => mc.Day.Equals("CurrentDay") && (mc.GetType().GetProperties().Where(p => p.PropertyType.Equals(typeof(bool)) && p.GetValue(this).Equals(true) && p.Name.Equals())).Equals(true));

        //if (hostelMessRule.AttendanceStartDateTime.Hour == 6 && hostelMessRule.AttendanceEndDateTime.Hour == 10)
        //{
        //    Attendance attendance = new Attendance();
        //    List<messMenu> messMenus = _db.Menu.AsEnumerable().Where(m => m.Day.Equals(DateTime.Now.DayOfWeek) || m.Day.Equals(DateTime.Now.AddDays(1).DayOfWeek)).Select(m => m).ToList();
        //    return View(attendance);
        //}
        //else if(hostelMessRule.AttendanceStartDateTime.Hour == 6 && hostelMessRule.AttendanceEndDateTime.Hour == 12)
        //{
        //    Attendance attendance = new Attendance();
        //    List<messMenu> messMenus = _db.Menu.AsEnumerable().Where(m => m.Day.Equals(DateTime.Now.DayOfWeek) || m.Day.Equals(DateTime.Now.AddDays(1).DayOfWeek)).Select(m => m).ToList();
        //    attendance.AttendanceDetails.AddRange(new List<AttendanceDetails>() { new AttendanceDetails() { messMenuId = messMenus.AsEnumerable().Where(m => m.Day.Equals(DateTime.Now.DayOfWeek)).Select(m => m).FirstOrDefault().MessMenuId, HostelitemessMenu = new messMenu() { Day = messMenus.AsEnumerable().Where(m => m.Day.Equals(DateTime.Now.DayOfWeek)).Select(m => m).FirstOrDefault().Day, Dinner =messMenus.AsEnumerable().Where(m => m.Day.Equals(DateTime.Now.DayOfWeek)).Select(m => m).FirstOrDefault().Dinner } }, new AttendanceDetails() { messMenuId = messMenus.AsEnumerable().Where(m => m.Day.Equals(DateTime.Now.AddDays(1).DayOfWeek)).Select(m => m).FirstOrDefault().MessMenuId, HostelitemessMenu = new messMenu() { Day = messMenus.AsEnumerable().Where(m => m.Day.Equals(DateTime.Now.AddDays(1).DayOfWeek)).Select(m => m).FirstOrDefault().Day, Breakfast = messMenus.AsEnumerable().Where(m => m.Day.Equals(DateTime.Now.AddDays(1).DayOfWeek)).Select(m => m).FirstOrDefault().Breakfast,Lunch=messMenus.AsEnumerable().Where(m=>m.Day.Equals(DateTime.Now.AddDays(1).DayOfWeek)).Select(m=>m).FirstOrDefault().Lunch } } });
        //    return View(attendance);
        //}
        //else if (hostelMessRule.AttendanceStartDateTime.Hour == 10 && hostelMessRule.AttendanceEndDateTime.Hour ==15)
        //{
        //    Attendance attendance = new Attendance();
        //    List<messMenu> messMenus = _db.Menu.AsEnumerable().Where(m => m.Day.Equals(DateTime.Now.DayOfWeek) || m.Day.Equals(DateTime.Now.AddDays(1).DayOfWeek)).Select(m => m).ToList();
        //    attendance.AttendanceDetails.AddRange(new List<AttendanceDetails>() { new AttendanceDetails() { messMenuId = messMenus.AsEnumerable().Where(m => m.Day.Equals(DateTime.Now.DayOfWeek)).Select(m => m).FirstOrDefault().MessMenuId, HostelitemessMenu = new messMenu() { Day = messMenus.AsEnumerable().Where(m => m.Day.Equals(DateTime.Now.DayOfWeek)).Select(m => m).FirstOrDefault().Day, Dinner = messMenus.AsEnumerable().Where(m => m.Day.Equals(DateTime.Now.DayOfWeek)).Select(m => m).FirstOrDefault().Dinner } }, new AttendanceDetails() { messMenuId = messMenus.AsEnumerable().Where(m => m.Day.Equals(DateTime.Now.AddDays(1).DayOfWeek)).Select(m => m).FirstOrDefault().MessMenuId, HostelitemessMenu = new messMenu() { Day = messMenus.AsEnumerable().Where(m => m.Day.Equals(DateTime.Now.AddDays(1).DayOfWeek)).Select(m => m).FirstOrDefault().Day, Breakfast = messMenus.AsEnumerable().Where(m => m.Day.Equals(DateTime.Now.AddDays(1).DayOfWeek)).Select(m => m).FirstOrDefault().Breakfast, Lunch = messMenus.AsEnumerable().Where(m => m.Day.Equals(DateTime.Now.AddDays(1).DayOfWeek)).Select(m => m).FirstOrDefault().Lunch } } });
        //    return View(attendance);
        //}
        //else if (hostelMessRule.AttendanceStartDateTime.Hour == 12 && hostelMessRule.AttendanceEndDateTime.Hour == 17)
        //{
        //    Attendance attendance = new Attendance();
        //    messMenu messMenu = _db.Menu.AsEnumerable().Where(m => m.Day.Equals(DateTime.Now.AddDays(1).DayOfWeek)).Select(m => m).FirstOrDefault();
        //    attendance.AttendanceDetails.AddRange(new List<AttendanceDetails>() { new AttendanceDetails() { messMenuId = messMenu.MessMenuId, HostelitemessMenu = messMenu } });
        //    return View(attendance);
        //}
        //else
        //{
        //    Attendance attendance = new Attendance();
        //    List<messMenu> messMenus = _db.Menu.AsEnumerable().Where(m => m.Day.Equals(DateTime.Now.AddDays(1).DayOfWeek) || m.Day.Equals(DateTime.Now.AddDays(2).DayOfWeek)).Select(m => m).ToList();
        //    attendance.AttendanceDetails.AddRange(new List<AttendanceDetails>() { new AttendanceDetails() { messMenuId = messMenus.AsEnumerable().Where(m => m.Day.Equals(DateTime.Now.AddDays(1).DayOfWeek)).Select(m => m).FirstOrDefault().MessMenuId, HostelitemessMenu = new messMenu() { Day = messMenus.AsEnumerable().Where(m => m.Day.Equals(DateTime.Now.AddDays(1).DayOfWeek)).Select(m => m).FirstOrDefault().Day, Dinner = messMenus.AsEnumerable().Where(m => m.Day.Equals(DateTime.Now.AddDays(1).DayOfWeek)).Select(m => m).FirstOrDefault().Dinner } }, new AttendanceDetails() { messMenuId = messMenus.AsEnumerable().Where(m => m.Day.Equals(DateTime.Now.AddDays(2).DayOfWeek)).Select(m => m).FirstOrDefault().MessMenuId, HostelitemessMenu = new messMenu() { Day = messMenus.AsEnumerable().Where(m => m.Day.Equals(DateTime.Now.AddDays(2).DayOfWeek)).Select(m => m).FirstOrDefault().Day, Breakfast = messMenus.AsEnumerable().Where(m => m.Day.Equals(DateTime.Now.AddDays(2).DayOfWeek)).Select(m => m).FirstOrDefault().Breakfast } } });
        //    attendance.Lunch = Lunch.NO;
        //    return View(attendance);
        //}


        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> Create(Attendance obj)
        {
            //if (obj.Name != obj.BreakFast || obj.Name != obj.Lunch || obj.Name != obj.Dinner)
            //{
            //    ModelState.AddModelError("Name", "The Display Order cannot exactly match Name");

            //}
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            // obj.hd=new HosteliteData() { Name=ob}
            obj.hd = _db.HosteliteDatas.Where(h => h.userid.Equals(user.Id)).Select(h => h).FirstOrDefault();
            //obj.NumberofAttendances = obj.GetType().GetProperties().Where(p => p.PropertyType.IsEnum && Convert.ToInt32(p.GetValue(obj)).Equals(0)).Select(p => p).Count();
            _db.Attendances.Add(obj);
            foreach (var ad in obj.AttendanceDetails)
            {
                _db.attendanceDetails.Add(ad);
                //_db.attendanceDetails.Attach(new AttendanceDetails() { messMenuId = ad.HostelitemessMenu.MessMenuId });
            }
            _db.SaveChanges();
            TempData["success"] = "Attendance created successfully!";
            //     return RedirectToAction("Index");
            return View(obj);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var attendanceFromDb = _db.Attendances.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id == id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (attendanceFromDb == null)
            {
                return NotFound();
            }
            return View(attendanceFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Attendance obj)
        {
            //if (obj.Name == obj.BreakFast || obj.Name == obj.Lunch || obj.Name == obj.Dinner)
            //{
            //    ModelState.AddModelError("Name", "The Display Order cannot exactly match the Name");
            //}
            if (ModelState.IsValid)
            {
                _db.Attendances.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Attendance updated successfully!";
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
            var attendanceFromDb = _db.Attendances.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id == id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (attendanceFromDb == null)
            {
                return NotFound();
            }
            return View(attendanceFromDb);
        }
        //POST
        [HttpPost, ActionName("DeletePOST")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Attendances.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Attendances.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Attendance deleted successfully!";
            return RedirectToAction("Index");

        }
       [HttpGet]
       public HostelMessRule CheckAttendanceTime()
        {
            
      
            return _db.HostelMessRules.FirstOrDefault();

        }

    }
}
