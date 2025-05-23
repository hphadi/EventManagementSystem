using System.Threading.Tasks; 
using EventManagementSystem.Data;
using EventManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace EventManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
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
        try
        {
            var events = await _context.Events.ToListAsync();
            return Ok(events);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error retrieving events: {ex.Message}\nInner Exception: {ex.InnerException?.Message}");
        }
    }

    // new Event
    [HttpPost]

    public async Task<IActionResult> CreateEvent(EventDto dto)
    {
        var newEvent = new Event
        {
            Title = dto.Title,
            Description = dto.Description,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            CreatedAt = dto.CreatedAt,
            Location = dto.Location
        };

        _context.Events.Add(newEvent);
        await _context.SaveChangesAsync();

        if (dto.GroupIds != null)
        {
            foreach (var groupId in dto.GroupIds)
            {
                _context.EventGroups.Add(new EventGroup
                {
                    EventId = newEvent.Id,
                    GroupId = groupId
                });
            }

            await _context.SaveChangesAsync();
        }
        
        return Ok(newEvent);
    }
    public async Task<ActionResult<Event>> CreateEvent(Event newEvent)
    {
        _context.Events.Add(newEvent);
        await _context.SaveChangesAsync();
        return Ok(newEvent);
    }

    // get Event with ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Event>> GetEvent(int id)
    {
        var eventEntity = await _context.Events
            .Include(e => e.EventGroups)
            .ThenInclude(eg => eg.Group)
            .FirstOrDefaultAsync(e => e.Id == id);
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

        //_context.EventGroups.Add(new EventGroup { EventId = eventId, GroupId = groupId });
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
