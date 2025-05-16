using System.Threading.Tasks; 
using EventManagementSystem.Data; 
using EventManagementSystem.Models; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore;

namespace EventManagementSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly AppDbContext _context;

    public EventController(AppDbContext context)
    {
        _context = context;
    }

    // get all Events
    [HttpGet]
    public async Task<ActionResult<List<Event>>> GetEvents()
    {
        var events = await _context.Events.ToListAsync();
        return Ok(events);
    }

    // new Event
    [HttpPost]
    public async Task<ActionResult<Event>> CreateEvent(Event newEvent)
    {
        _context.Events.Add(newEvent);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetEvent), new { id = newEvent.Id }, newEvent);
    }

    // get Event with ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Event>> GetEvent(int id)
    {
        var eventEntity = await _context.Events.FindAsync(id);
        if (eventEntity == null) return NotFound();
        return eventEntity;
    }

    // assign Event to Group
    [HttpPost("{eventId}/assign-group/{groupId}")]
    public async Task<IActionResult> AssignEventToGroup(int eventId, int groupId)
    {
        var eventEntity = await _context.Events.FindAsync(eventId);
        var group = await _context.Groups.FindAsync(groupId);
        if (eventEntity == null || group == null) return NotFound();

        _context.EventGroups.Add(new EventGroup { EventId = eventId, GroupId = groupId });
        await _context.SaveChangesAsync();
        return NoContent();
    }
}