using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace BulkyBookWeb.Models
{

    public class AttendanceDetails
    {
        
        public int AttendaceId { get; set; }
        public virtual Attendance HosteliteAttendance { get; set; }
        public int messMenuId { get; set; }
        public virtual messMenu HostelitemessMenu {get;set;}
    }
}