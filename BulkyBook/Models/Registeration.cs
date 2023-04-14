using System.ComponentModel.DataAnnotations;
namespace BulkyBookWeb.Models
{
    public class Registeration
    {
        public Registeration()
        {
           
        }
        [Required(ErrorMessage = "*")]
        [StringLength(20, MinimumLength = 5)]
        [Display(Name = "UserName")]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Conform Password")]
        [Compare("Password",ErrorMessage ="Password and Conformation Password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
