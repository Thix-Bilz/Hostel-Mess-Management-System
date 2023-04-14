using System.ComponentModel.DataAnnotations;

namespace BulkyBookWeb.Models
{
    public class EditRoleViewModel
    {
        public EditRoleViewModel()
        {
            users = new List<string>();
        }
        public string Id { get; set; }
        [Required(ErrorMessage ="Role Name is Required")]

        public string RoleName { get; set; }
        public List<string> users { get; set; }
    }
}
