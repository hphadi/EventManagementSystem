using Microsoft.EntityFrameworkCore;
using EventManagementSystem.Models;
using System;

namespace EventManagementSystem.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Person> People => Set<Person>();
    public DbSet<Group> Groups => Set<Group>();
    public DbSet<Event> Events => Set<Event>();

}