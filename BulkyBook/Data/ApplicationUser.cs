using Microsoft.AspNetCore.Identity;

namespace BulkyBookWeb.Data
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ApplicationRole Role { get; set; }
    }
}
