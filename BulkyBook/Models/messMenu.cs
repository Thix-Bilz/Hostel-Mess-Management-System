using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBookWeb.Models
{
    public class messMenu
    {
        public messMenu()
        {
            AttendanceDetails = new List<AttendanceDetails>();
        }

    [Key]
        public int MessMenuId { get; set; }
        [Required]
        public DayOfWeek Day { get; set; }
        public string? Breakfast { get; set; }
        [Required]
        public decimal BreakFastPrice { get; set; }
        public string? Lunch { get; set; }
        [Required]
        public decimal LunchPrice { get; set; }
        public string? Dinner { get; set; }
        [Required]
        public decimal DinnerPrice { get; set; }
        
        public int? MessMenuCofigId { get; set; }
        public virtual MessMenuConfiguration MessMenuConfiguration { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public virtual List<AttendanceDetails> AttendanceDetails { get; set; }
    }
}
