using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
namespace BulkyBookWeb.Models
{
    public class MessMenuConfiguration
    {
        [Key]
        public int Id { get; set; }
        public string Day { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public DateTime DateTime { get; set; }
    
        public BreakFast Breakfast { get; set; }
   
        public Lunch Lunch { get; set; }
     
        public Dinner Dinner { get; set; }
        public virtual List<messMenu> MessMenus { get; set; }
    }
}
