
using BulkyBookWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace BulkyBookWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser,IdentityRole,string>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<messMenu> Menu { get; set; }
        public DbSet<HosteliteData> HosteliteDatas { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<ImageModel> Images { get; set; }
        public DbSet<HostelMessRule> HostelMessRules { get; set; }
        public DbSet<NamazTiming> NamazTimings { get; set; }
        public DbSet<AttendanceDetails> attendanceDetails { get; set; }
        public DbSet<MessMenuConfiguration> messMenuConfigurations { get; set; }
        public DbSet<MessMenuPrices> messMenuPrices { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    //(localdb)\\mssqllocaldb
        //    optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Bulky");
            
        //}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //One to Many
            //builder.Entity<HosteliteData>().HasOne<Attendance>().WithMany().HasForeignKey().OnDelete(DeleteBehavior.Cascade);
            //One to One
            builder.Entity<AttendanceDetails>().HasKey(ad => new {ad.AttendaceId,ad.messMenuId});
            builder.Entity<AttendanceDetails>().HasOne(ad => ad.HosteliteAttendance).WithMany(a=>a.AttendanceDetails).HasForeignKey(ad => ad.AttendaceId);
            builder.Entity<AttendanceDetails>().HasOne(ad => ad.HostelitemessMenu).WithMany(m=>m.AttendanceDetails).HasForeignKey(ad=>ad.messMenuId);
            builder.Entity<IdentityUser>()
               .HasOne<HosteliteData>()
               .WithOne(h => h.User).HasForeignKey<HosteliteData>(x => x.userid);
            builder.Entity<messMenu>().HasOne(mm => mm.MessMenuConfiguration).WithMany(mc => mc.MessMenus).HasForeignKey(mm => mm.MessMenuCofigId);
            builder.Entity<ImageModel>().HasOne(img =>img.HosteliteData).WithMany(hd =>hd.ImageModels ).HasForeignKey(img =>img.hosteliteId);
        }
        

    }
}
