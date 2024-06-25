using Microsoft.AspNetCore.Identity;

namespace AnketPortali01.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
