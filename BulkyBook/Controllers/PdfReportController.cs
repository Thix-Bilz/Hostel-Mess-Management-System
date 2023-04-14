using Microsoft.AspNetCore.Mvc;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Drawing;
using Syncfusion.Pdf.Grid;
using System.IO;
using BulkyBookWeb.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using BulkyBookWeb.Models;
using System.Linq;
using Syncfusion.Pdf.Parsing;
namespace BulkyBookWeb.Controllers
{
    public class PdfReportController : Controller
    {
        private readonly UserManager<IdentityUser> _usermanager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext applicationDbContext;
        public PdfReportController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,ApplicationDbContext dbContext)
        {
            _usermanager = userManager;
            _signInManager = signInManager;
            applicationDbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<FileStreamResult> CreateDocument()
        {
            ////Create a new PDF document
            //PdfDocument document = new PdfDocument();

            ////Add a page to the document
            //PdfPage page = document.Pages.Add();

            ////Create PDF graphics for the page
            //PdfGraphics graphics = page.Graphics;

            ////Set the standard font
            //PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

            ////Draw the text
            //graphics.DrawString("Hello World!!!", font, PdfBrushes.Black, new PointF(0, 0));

            ////Saving the PDF to the MemoryStream
            //MemoryStream stream = new MemoryStream();

            //document.Save(stream);

            ////Set the position as '0'.
            //stream.Position = 0;

            ////Download the PDF document in the browser
            //FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/pdf");

            //fileStreamResult.FileDownloadName = "Sample.pdf";

            //return fileStreamResult;





            //Create Pdf Document with Image

            ////Create a new PDF document.
            //PdfDocument doc = new PdfDocument();
            ////Add a page to the document.
            //PdfPage page = doc.Pages.Add();
            ////Create PDF graphics for the page
            //PdfGraphics graphics = page.Graphics;
            ////Load the image as stream.
            //FileStream imageStream = new FileStream(@"C:\Users\Shahid Iqbal\OneDrive\Pictures\Camera Roll\WIN_20210921_12_01_47_Pro.jpg", FileMode.Open, FileAccess.Read);
            //PdfBitmap image = new PdfBitmap(imageStream);
            ////Draw the image
            //graphics.DrawImage(image, 0, 0);
            ////Save the PDF document to stream
            //MemoryStream stream = new MemoryStream();
            //doc.Save(stream);
            ////If the position is not set to '0' then the PDF will be empty.
            //stream.Position = 0;
            ////Close the document.
            //doc.Close(true);
            ////Defining the ContentType for pdf file.
            //string contentType = "application/pdf";
            ////Define the file name.
            //string fileName = "Output.pdf";
            ////Creates a FileContentResult object by using the file contents, content type, and file name.
            //return File(stream, contentType, fileName);







            //Creating a PDF document with table

            //Create a new PDF document.
            PdfDocument doc = new PdfDocument();
            //Add a page.
            PdfPage page = doc.Pages.Add();
            //Create a PdfGrid.
            PdfGrid pdfGrid = new PdfGrid();
            //Add values to list
            //List<object> data = new List<object>();
            //Object row1 = new { ID = "E01", Name = "Clay" };
            //Object row2 = new { ID = "E02", Name = "Thomas" };
            //Object row3 = new { ID = "E03", Name = "Andrew" };
            //Object row4 = new { ID = "E04", Name = "Paul" };
            //Object row5 = new { ID = "E05", Name = "Gray" };
            //data.Add(row1);
            //data.Add(row2);
            //data.Add(row3);
            //data.Add(row4);
            //data.Add(row5);
            //Add list to IEnumerable
            //IEnumerable<object> dataTable = data;
            //string name = User.Identity.Name;
            ////Assign data source.

            //from a in applicationDbContext.Attendances
            //where a.hd.Id == id && a.CreatedDateTime == DateTime.Now
            //select a;

            Attendance attendance = new Attendance();
            List<HosteliteData> hosteliteDatas = new List<HosteliteData>();
            IEnumerable<HosteliteData> datas = hosteliteDatas;
            Attendance attendance1 = new Attendance();
            var user = await _usermanager.FindByNameAsync(User.Identity.Name);
            pdfGrid.DataSource = applicationDbContext.Attendances.Include(a => a.hd).Include(a => a.hd.User).Include(a => a.AttendanceDetails).Where(a =>
                         a.hd.User.Id == user.Id && a.CreatedDateTime.Month == DateTime.Now.Month).Select(a =>

                        new
                        {
                            Id = a.Id,
                            Name = a.Name,
                            BreakFast = a.BreakFast,
                            Lunch = a.Lunch,
                            Dinner = a.Dinner,
                            RoomNumber = a.hd.RoomNumber,
                            Total=a.AttendanceDetails.Select(ad=>applicationDbContext.Menu.Where(mm=>ad.messMenuId.Equals(mm.MessMenuId)).Select(mm =>(a.BreakFast.Equals(BreakFast.Yes)?mm.BreakFastPrice:0)+(a.Lunch.Equals(Lunch.Yes)?mm.LunchPrice:0)+(a.Dinner.Equals(Dinner.Yes)?mm.DinnerPrice:0)).FirstOrDefault()).FirstOrDefault()
                            //Day = a.MessMenu.Day,
                            // Total=a.MessMenu.Menuprice*a.NumberofAttendances
                        });
            
       // pdfGrid.DataSource = applicationDbContext.HosteliteDatas.Join(applicationDbContext.Attendances, h => h.Id, a => a.hd.Id, (h, a) => new { Name = h.Name, BreakFast = a.BreakFast, Lunch = a.Lunch, Dinner = a.Dinner, Date = a.CreatedDateTime });
       //pdfGrid.DataSource = applicationDbContext.HosteliteDatas.GroupJoin(applicationDbContext.Attendances.Where(a=>a.hd.Id==user.Id), h => h.Id, a => a.hd.Id, (h, a) => new 
       //{ 
       //    hostelite = h, 
       //    Attendaces = a 
       
       //});
            
                

                //Draw grid to the page of PDF document.
                pdfGrid.Draw(page, new PointF(10, 10));
                //Save the PDF document to stream
                MemoryStream stream = new MemoryStream();
                doc.Save(stream);
                //If the position is not set to '0' then the PDF will be empty.
                stream.Position = 0;
                //Close the document.
                doc.Close(true);
                //Defining the ContentType for pdf file.
                string contentType = "application/pdf";
                //Define the file name.
                string fileName = "Output.pdf";
                //Creates a FileContentResult object by using the file contents, content type, and file name.
            return File(stream, contentType, fileName);
        }
        private static int Add(int x, int y) { return x + y; }


    }
}
