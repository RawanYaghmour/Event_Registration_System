using Event_Registration_System.Models;

namespace Event_Registration_System.Repository.Interfaces
{
    public interface IEvent
    {
        public List<Event> GetAllEvent();
        public Event GetEvent(int id);
        public Event CreateEvent(Event events);
        public Event UpdateEvent(Event events);
        public Event DeleteEvent(int id);
    }
}
