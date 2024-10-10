using Event_Registration_System.Models;
using Mailjet.Client.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace Event_Registration_System.Data
{
    public class RegistrationDBContext : IdentityDbContext<User>
    {
        public RegistrationDBContext(DbContextOptions<RegistrationDBContext> options) : base(options)
        {
        }

        public DbSet<Models.User> users { get; set; }
        public DbSet<Event> events { get; set; }
        public DbSet<Registration> registrations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Registration>()
                .HasOne(r => r.Event)
                .WithMany(e => e.Registrations)
                .HasForeignKey(r => r.EventId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Registration>()
                .HasOne(r => r.User)
                .WithMany(u => u.Registrations)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Event>().HasData(
               new Event
               {
                   EventId = 1,
                   Title = "Tech Conference 2024",
                   Date = new DateTime(2024, 12, 1),
                   Description = "An annual conference focusing on the latest technology trends.",
                   Capacity = 300
               },
               new Event
               {
                   EventId = 2,
                   Title = "Startup Pitch Night",
                   Date = new DateTime(2024, 11, 15),
                   Description = "An event where startups present their ideas to investors.",
                   Capacity = 100
               },
               new Event
               {
                   EventId = 3,
                   Title = "Workshop: AI for Beginners",
                   Date = new DateTime(2024, 10, 20),
                   Description = "A hands-on workshop introducing the basics of AI.",
                   Capacity = 50
               }
           );
        }
    }
}
