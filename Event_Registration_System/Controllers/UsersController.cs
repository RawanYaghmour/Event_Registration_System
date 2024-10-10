using Event_Registration_System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Event_Registration_System.Models;


namespace Event_Registration_System.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly RegistrationDBContext _context;

        public UsersController(RegistrationDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers(string searchString)
        {
            var result = _context.users.ToList();
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserById(string id)
        {
            var book = await _context.users.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var book = await _context.users.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(string id, User book)
        {
            var existingBook = await _context.users.FindAsync(id);
            if (existingBook == null)
            {
                return NotFound();
            }

            // Update book properties
            existingBook.Email = book.Email;
            existingBook.UserName = book.UserName;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Error updating the user. Please try again later.");
            }

            return RedirectToAction("GetUsers");
        }
        [HttpGet]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var book = await _context.users.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book); // Show the book details to confirm deletion
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUserConfirmed(string id)
        {
            var book = await _context.users.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            _context.users.Remove(book);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(GetUsers));
        }
    }
}
