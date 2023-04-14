using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
namespace BulkyBookWeb.Models

{
    public class ModelsHelper
    {
        public List<HosteliteData> GetHosteliteDatas()
        {

            var options = new DbContextOptions<ApplicationDbContext>();
            ApplicationDbContext applicationDbContext = new ApplicationDbContext(options);
            
            List<HosteliteData> hosteliteDatas=new List<HosteliteData> ();
          foreach(var hd in applicationDbContext.HosteliteDatas)
            {
                hosteliteDatas.Add(hd);
            }
            return hosteliteDatas;
        }
        public List<ImageModel> GetImages()
        {

            var options = new DbContextOptions<ApplicationDbContext>();
            ApplicationDbContext applicationDbContext = new ApplicationDbContext(options);
            List<ImageModel> Imagesdata = new List<ImageModel>();
            foreach (var Imgd in applicationDbContext.Images)
            {
                Imagesdata.Add(Imgd);
            }
            return Imagesdata;
        }
     
    }
}
