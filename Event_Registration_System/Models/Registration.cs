using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Event_Registration_System.Models
{
    public class Registration
    {
        public int RegistrationId { get; set; }

        [Required(ErrorMessage = "ParticipantName is required")]
        public string ParticipantName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        public int EventId { get; set; }
        public string UserId { get; set; }
        public Event Event { get; set; }
        public User User { get; set; }
    }
}
