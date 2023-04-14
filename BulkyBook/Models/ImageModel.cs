using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BulkyBookWeb.Models
{
    public class ImageModel
    {
        [Key]
        public int ImageId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string? Title { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Image Name")]
        public string? ImageName { get; set; }
        public string? ImageUrl { get; set; }
        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile? ImageFile { get; set; }
        public string hosteliteId { get; set; }
        public virtual HosteliteData HosteliteData{ get; set; }
      
    }
}
