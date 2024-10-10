using Microsoft.AspNetCore.Identity;

namespace Event_Registration_System.Models
{
    public class User : IdentityUser
    {
        public ICollection<Registration> Registrations { get; set; }

    }
}
