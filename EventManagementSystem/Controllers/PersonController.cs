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
    public async Task<IActionResult> Register(Person person)
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
    public async Task<IActionResult> Login(Person loginRequest)
    {
        var person = await _context.People
            .FirstOrDefaultAsync(p => p.Username == loginRequest.Username && p.Password == loginRequest.Password);

        if (person == null)
        {
            return Unauthorized("Invalid username or password.");
        }

        // Optionally remove sensitive data (e.g. password) before returning
        person.Password = "";
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
}
