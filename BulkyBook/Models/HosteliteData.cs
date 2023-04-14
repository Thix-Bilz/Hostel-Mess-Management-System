using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulkyBookWeb.Models
{
    
    public class HosteliteData
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public string Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [DisplayName("Room Number")]
        [Range(100, 500, ErrorMessage = "Room Number must be between 101 and 321 only!!")]
        public int RoomNumber { get; set; }
        public string? Department { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
       
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public string userid { get; set; }
        public virtual IdentityUser User { get; set; }
        public virtual List<Attendance> Attendances { get; set; }
        public virtual List<ImageModel> ImageModels { get; set; }
       // public static List<Attendance> GetAttendances() => new List<Attendance>() { new Attendance() {Id=1,Name="Shahid" }, new Attendance() { Id = 2, Name = "Bilal" } };
    }
}
