using Event_Registration_System.Models;
using Event_Registration_System.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Event_Registration_System.Controllers
{
    public class EventsController : Controller
    {
        private readonly IEvent _context;
        public EventsController(IEvent context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents(string searchString)
        {
            var allEvents = _context.GetAllEvent();
            return View(allEvents);
        }

        [HttpGet]
        public IActionResult AddEvent()
        {
            Event even = new Event();
            return View(even);
        }

        [HttpPost]
        public async Task<IActionResult> AddEvent(Event events)
        {
            _context.CreateEvent(events);
            return RedirectToAction("GetEvents");
        }

        [HttpGet]
        public async Task<IActionResult> GetEventById(int id)
        {
            var getById = _context.GetEvent(id);
            return View(getById);
        }

        [HttpGet]
        public async Task<IActionResult> EditEvent(int id)
        {
            Event even = new Event();
            return View(even);
        }

        [HttpPost]
        public async Task<IActionResult> EditEvent(int id, Event events)
        {
            _context.UpdateEvent(events);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEventConfirmed(int id)
        {
            _context.DeleteEvent(id);
            return RedirectToAction("Index");
        }
    }
}
