using Event_Registration_System.Data;
using Event_Registration_System.Models;
using Event_Registration_System.Repository.Serveses;
using Microsoft.AspNetCore.Mvc;

namespace Event_Registration_System.Controllers
{
    public class RegistraionsController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly RegistrationDBContext _context;

        public RegistraionsController(IConfiguration configuration, RegistrationDBContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public IActionResult Index()
        {
            // Fetch registrations from the database and pass them to the view
            var registrations = _context.registrations.ToList();
            return View(registrations);
        }

        [HttpPost]
        public IActionResult Index(int registrationId)
        {
            // Find the registration based on the provided registrationId
            var registration = _context.registrations.Where(a => a.RegistrationId == registrationId).First();

            if (registration != null)
            {
                // Create an instance of MailjetService and send the email
                EmailService service = new EmailService(_configuration);
                service.SendEmailAsync(
                     registration.Email,
                     registration.ParticipantName,
                     "<h1>Your registration has been approved!</h1>");
            }

            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {
            Registration regester = new Registration();
            return View(regester);
        }
        [HttpPost]
        public IActionResult Create(Registration registration)
        {
            var regester = new Registration()
            {
                RegistrationId = registration.RegistrationId,
                Email = registration.Email,
                ParticipantName = registration.ParticipantName,
                EventId = registration.EventId,
                UserId = registration.UserId
            };
            _context.registrations.Add(regester);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
