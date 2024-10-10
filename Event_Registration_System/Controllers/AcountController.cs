using Event_Registration_System.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Event_Registration_System.Data;
using Event_Registration_System.Repository.Interfaces;
using Microsoft.Identity.Client;

namespace Event_Registration_System.Controllers
{
    public class AcountController : Controller
    {
        private readonly IAccount _contextt;

        public AcountController(IAccount context)
        {
            _contextt = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserDTO data)
        {
            if (ModelState.IsValid)
            {
                var user = await _contextt.Register(data, ModelState);

                if (user != null)
                {
                    // After registration, redirect to login or dashboard
                    return RedirectToAction("Index", "Home");
                }
            }

            // If model state is invalid or registration fails, return to the view
            return View(data);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return View(loginDto);
            }

            var user = await _contextt.LoginUser(loginDto.Username, loginDto.Password);

            if (user == null)
            {
                // Add error message for invalid login
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(loginDto);
            }

            // On successful login, redirect to the home or dashboard
            return RedirectToAction("Index");
        }
    }
}
