using System.ComponentModel.DataAnnotations;

namespace BulkyBookWeb.Models
{
    public class NamazTiming
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Namaz { get; set; }
        public string? Namaztiming { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}

