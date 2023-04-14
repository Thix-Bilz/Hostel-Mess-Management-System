using System.ComponentModel.DataAnnotations;

namespace BulkyBookWeb.Models
{
    public class CreateRoleViewModel
    {
        [Required]
        [Key]
        public string roleName { get; set; }
    }
}
