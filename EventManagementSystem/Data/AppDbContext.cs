using Microsoft.EntityFrameworkCore;
using EventManagementSystem.Models;

namespace EventManagementSystem.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Event> Events { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Person> People { get; set; }
    //public DbSet<EventGroup> EventGroups { get; set; }
    //public DbSet<GroupMember> GroupMembers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<EventGroup>()
        //    .HasKey(eg => new { eg.EventId, eg.GroupId });

        //modelBuilder.Entity<EventGroup>()
        //    .HasOne(eg => eg.Event)
        //    .WithMany(e => e.EventGroups)
        //    .HasForeignKey(eg => eg.EventId);

        //modelBuilder.Entity<EventGroup>()
        //    .HasOne(eg => eg.Group)
        //    .WithMany(g => g.EventGroups)
        //    .HasForeignKey(eg => eg.GroupId);

        //modelBuilder.Entity<GroupMember>()
        //    .HasKey(gm => new { gm.GroupId, gm.PersonId });

        //modelBuilder.Entity<GroupMember>()
        //    .HasOne(gm => gm.Group)
        //    .WithMany(g => g.GroupMembers)
        //    .HasForeignKey(gm => gm.GroupId);

        //modelBuilder.Entity<GroupMember>()
        //    .HasOne(gm => gm.Person)
        //    .WithMany(p => p.GroupMembers)
        //    .HasForeignKey(gm => gm.PersonId);
    }
}