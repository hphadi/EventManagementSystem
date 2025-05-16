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

    [HttpPost]
    public async Task<IActionResult> AddPerson(Person person)
    {
        // Don't set CreatedAt manually
        _context.People.Add(person);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPeople), new { id = person.Id }, person);
    }

    [HttpGet]
    public async Task<IActionResult> GetPeople()
    {
        var people = await _context.People.ToListAsync();
        return Ok(people);
    }
}
