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
    public async Task<ActionResult<List<EventBase>>> GetEvents()
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

    [HttpGet("{id}")]
    public async Task<ActionResult<EventWithGroupsDto>> GetEventDetails(int id)
    {
        var ev = await _context.Events
            .Where(e => e.Id == id) // ✅ Efficient and index-friendly
            .Select(e => new EventWithGroupsDto
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                CreatedAt = e.CreatedAt,
                Location = e.Location,
                Groups = e.EventGroups
                    .Select(eg => new SimpleGroupDto
                    {
                        Id = eg.Group.Id,
                        Name = eg.Group.Name,
                        City = eg.Group.City
                    }).ToList()
            })
            .FirstOrDefaultAsync();

        return ev == null ? NotFound() : Ok(ev);
    }
}
