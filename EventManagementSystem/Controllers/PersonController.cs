using EventManagementSystem.Data;
using EventManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    private readonly AppDbContext _context;

    public PersonController(AppDbContext context)
    {
        _context = context;
    }

    // ✅ Sign up (registration)
    [HttpPost("register")]
    public async Task<IActionResult> Register(Person_ person)
    {
        // Check for duplicate username
        if (await _context.People.AnyAsync(p => p.Username == person.Username))
        {
            return Conflict("Username already exists.");
        }

        _context.People.Add(person);
        await _context.SaveChangesAsync();
        return Ok(person);
    }

    // ✅ Sign in (authentication)
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]  LoginDto loginRequest)
    {
        var person = await _context.People
            .FirstOrDefaultAsync(p => p.Username == loginRequest.Username && p.Password == loginRequest.Password);

        if (person == null)
            return Unauthorized("Invalid credentials");

        person.Password = ""; // hide password if returning person
        return Ok(person);
    }

    // ✅ Delete user
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var person = await _context.People.FindAsync(id);
        if (person == null)
        {
            return NotFound();
        }

        _context.People.Remove(person);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PersonWithDetailsDto>> GetPersonDetails(string id)
    {
        var person = await _context.People
            .Where(g => g.Id.ToString() == id)
            .Include(g => g.EventPersons)
            .ThenInclude(eg => eg.Event)
            .Select(g => new PersonWithDetailsDto
            {
                Id = g.Id,
                Name = g.Name,
                Username = g.Username,
                Events = g.EventPersons
                    .Select(eg => new SimpleEventDto
                    {
                        Id = eg.Event.Id,
                        Title = eg.Event.Title,
                        StartDate = eg.Event.StartDate,
                        Location = eg.Event.Location
                    }).ToList(),
                Groups = g.GroupPersons
                    .Select(eg => new SimpleGroupDto
                    {
                        Id = eg.Group.Id,
                        Name = eg.Group.Name,
                        City = eg.Group.City
                    }).ToList()
            })
            .FirstOrDefaultAsync();

        if (person == null)
        {
            return NotFound();
        }

        return Ok(person);
    }
}
