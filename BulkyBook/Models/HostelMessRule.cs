using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulkyBookWeb.Models
{
    public class HostelMessRule
    {
        //[Required]
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public string Id { get; set; }
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string id { get; set; }
        [Required]
        public string? singleRule { get; set; }
        [Required]
        public DateTime AttendanceStartDateTime { get; set; }
        [Required]
        public DateTime AttendanceEndDateTime { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}

