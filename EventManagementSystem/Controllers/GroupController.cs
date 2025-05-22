using EventManagementSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventManagementSystem.Models;

namespace EventManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GroupController : ControllerBase
{
    private readonly AppDbContext _context;

    public GroupController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> AddGroup(Group group)
    {
        // Don't set CreatedAt manually
        _context.Groups.Add(group);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetGroups), new { id = group.Id }, group);
    }

    [HttpGet]
    public async Task<IActionResult> GetGroups()
    {
        var groups = await _context.Groups.ToListAsync();
        return Ok(groups);
    }

    [HttpGet("{id}/events")]
    public async Task<ActionResult<IEnumerable<Event>>> GetEventsByGroup(int id)
    {
        var group = await _context.Groups
            .Include(g => g.EventGroups)
            .ThenInclude(eg => eg.Event)
            .FirstOrDefaultAsync(g => g.Id == id);

        if (group == null)
        {
            return NotFound();
        }

        var events = group.EventGroups.Select(eg => eg.Event).ToList();
        return Ok(events);
    }
}

