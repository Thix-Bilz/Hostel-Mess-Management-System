using Microsoft.AspNetCore.Identity;

namespace BulkyBookWeb.Data
{
    public class ApplicationRole  : IdentityRole
    {
        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
