using EventRegistrationAPI_ArtemKotliar.Model;
using Microsoft.EntityFrameworkCore;

namespace EventRegistrationAPI_ArtemKotliar.Data;

/// <summary>
/// DbContext to access the sqlite database. 
/// </summary>
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    //Provides the access to the Events and Registration tables in the database with EF Core. 
    public DbSet<Event> Events { get; set; }
    public DbSet<Registration> Registrations { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed data
        modelBuilder.Entity<Event>().HasData(
            new Event
            {
                EventId = 1,
                Title = "New Student Orientation",
                Location = "Main Auditorium",
                EventDate = new DateTime(
                    2026, 9, 10, 14, 0, 0, DateTimeKind.Utc),
                Capacity = 100
            },
            new Event
            {
                EventId = 2,
                Title = "Cloud Computing Workshop",
                Location = "Computer Lab A",
                EventDate = new DateTime(
                    2027, 6, 15, 15, 0, 0, DateTimeKind.Utc),
                Capacity = 30
            },
            new Event
            {
                EventId = 3,
                Title = "Small Group Career Coaching",
                Location = "Meeting Room B",
                EventDate = new DateTime(
                    2027, 7, 20, 16, 0, 0, DateTimeKind.Utc),
                Capacity = 2
            },
            new Event
            {
                EventId = 4,
                Title = "Campus Info Fair",
                Location = "Student Centre",
                EventDate = new DateTime(
                    2027, 8, 12, 17, 0, 0, DateTimeKind.Utc),
                Capacity = 200
            }
        );

        // Event 3 starts full
        modelBuilder.Entity<Registration>().HasData(
            new Registration
            {
                RegistrationId = 1,
                EventId = 3,
                StudentName = "Artem Kotliar",
                Email = "artem.kotliar@example.com",
                RegisteredOn = new DateTime(
                    2026, 11, 20, 14, 0, 0, DateTimeKind.Utc)
            },
            new Registration
            {
                RegistrationId = 2,
                EventId = 3,
                StudentName = "Ariston Michael",
                Email = "ariston.michael@example.com",
                RegisteredOn = new DateTime(
                    2026, 11, 21, 15, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}