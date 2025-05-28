using Microsoft.EntityFrameworkCore;
using EventManagementSystem.Models;

namespace EventManagementSystem.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Event> Events { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Person_> People { get; set; }
    public DbSet<EventGroup> EventGroups { get; set; }
    public DbSet<EventPerson> EventPersons { get; set; }
    public DbSet<GroupPerson> GroupPersons { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EventGroup>()
            .HasKey(eg => new { eg.EventId, eg.GroupId });

        modelBuilder.Entity<EventGroup>()
            .HasOne(eg => eg.Event)
            .WithMany(e => e.EventGroups)
            .HasForeignKey(eg => eg.EventId);

        modelBuilder.Entity<EventGroup>()
            .HasOne(eg => eg.Group)
            .WithMany(g => g.EventGroups)
            .HasForeignKey(eg => eg.GroupId);
        
        modelBuilder.Entity<EventPerson>()
            .HasKey(ep => new { ep.EventId, ep.PersonId });

        modelBuilder.Entity<EventPerson>()
            .HasOne(ep => ep.Event)
            .WithMany(e => e.EventPersons)
            .HasForeignKey(ep => ep.EventId);

        modelBuilder.Entity<EventPerson>()
            .HasOne(ep => ep.Person)
            .WithMany(p => p.EventPersons)
            .HasForeignKey(ep => ep.PersonId);
        modelBuilder.Entity<GroupPerson>()
            .HasKey(gp => new { gp.GroupId, gp.PersonId });

        modelBuilder.Entity<GroupPerson>()
            .HasOne(gp => gp.Group)
            .WithMany(g => g.GroupPersons)
            .HasForeignKey(gp => gp.GroupId);

        modelBuilder.Entity<GroupPerson>()
            .HasOne(gp => gp.Person)
            .WithMany(p => p.GroupPersons)
            .HasForeignKey(gp => gp.PersonId);

    }
}