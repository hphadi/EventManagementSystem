using Microsoft.EntityFrameworkCore;
using EventManagementSystem.Models;

namespace EventManagementSystem.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Event> Events { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Person_> People { get; set; }
    public DbSet<EventGroup> EventGroups { get; set; }
    public DbSet<EventPerson> EventPersons { get; set; }
    public DbSet<GroupPerson> GroupPersons { get; set; }
    //public DbSet<GroupMember> GroupMembers { get; set; }

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<EventGroup>()
    //    .HasKey(eg => new { eg.EventId, eg.GroupId });

    //    //modelBuilder.Entity<EventGroup>()
    //    //    .HasOne(eg => eg.Event)
    //    //    .WithMany(e => e.EventGroups)
    //    //    .HasForeignKey(eg => eg.EventId);

    //    modelBuilder.Entity<EventGroup>()
    //        .HasOne(eg => eg.Group)
    //        .WithMany(g => g.EventGroups)
    //        .HasForeignKey(eg => eg.GroupId);

    //    //modelBuilder.Entity<GroupMember>()
    //    //    .HasKey(gm => new { gm.GroupId, gm.PersonId });

    //    //modelBuilder.Entity<GroupMember>()
    //    //    .HasOne(gm => gm.Group)
    //    //    .WithMany(g => g.GroupMembers)
    //    //    .HasForeignKey(gm => gm.GroupId);

    //    //modelBuilder.Entity<GroupMember>()
    //    //    .HasOne(gm => gm.Person)
    //    //    .WithMany(p => p.GroupMembers)
    //    //    .HasForeignKey(gm => gm.PersonId);
    //}

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