using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulkyBookWeb.Models
{
    public class Attendance
    {
        public Attendance()
        {
            AttendanceDetails = new List<AttendanceDetails>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Please mention Yes or No")]
        public  BreakFast BreakFast { get; set; }
        [Required(ErrorMessage = "Please mention Yes or No")]
        public Lunch Lunch { get; set; }
        [Required(ErrorMessage = "Please mention Yes or No")]
        public Dinner Dinner{ get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public virtual HosteliteData hd { get; set; }
       
       public virtual List<AttendanceDetails> AttendanceDetails { get; set; }
        //public List<Attendance> GetAttendances() => new List<Attendance>();
        
    }
}
