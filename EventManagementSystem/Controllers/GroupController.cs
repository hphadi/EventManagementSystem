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
    public async Task<IActionResult> AddGroup(GroupBase group)
    {
        // Don't set CreatedAt manually
        var newGroup = new Group
        {
            Name = group.Name,
            City = group.City,
            Description = group.Description,
            CreatedAt = group.CreatedAt
        };
        try
        {
            Console.WriteLine($"Trying to save group: {group.Name}");
            _context.Groups.Add(newGroup);
            await _context.SaveChangesAsync();
            Console.WriteLine("Group saved successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving group: {ex.Message}");
            return StatusCode(500, ex.Message);
        }

        return Ok(newGroup);
    }

    [HttpGet]
    public async Task<IActionResult> GetGroups()
    {
        var groups = await _context.Groups.ToListAsync();
        return Ok(groups);
    }

    //[HttpGet("{id}/events")]
    //public async Task<ActionResult<IEnumerable<Event>>> GetEventsByGroup(int id)
    //{
    //    var group = await _context.Groups
    //        .Where(g => g.Id == id)
    //        .Select(g => new {
    //            g.Id,
    //            g.Name,
    //            EventGroups = g.EventGroups.Select(eg => new {
    //                eg.EventId,
    //                Event = new
    //                {
    //                    eg.Event.Id,
    //                    eg.Event.Title,
    //                    eg.Event.StartDate
    //                }
    //            })
    //        })
    //        .FirstOrDefaultAsync();

    //    if (group == null)
    //    {
    //        return NotFound();
    //    }

    //    var events = group.EventGroups.Select(eg => eg.Event).ToList();
    //    return Ok(events);
    //}

    [HttpGet("{id}")]
    public async Task<ActionResult<GroupWithEventsDto>> GetGroupDetails(int id)
    {
        var group = await _context.Groups
            .Where(g => g.Id == id)
            .Include(g => g.EventGroups)
            .ThenInclude(eg => eg.Event)
            .Select(g => new GroupWithEventsDto
            {
                Id = g.Id,
                Name = g.Name,
                City = g.City,
                Description = g.Description,
                Events = g.EventGroups
                    .Select(eg => new SimpleEventDto
                    {
                        Id = eg.Event.Id,
                        Title = eg.Event.Title,
                        StartDate = eg.Event.StartDate,
                        Location = eg.Event.Location
                    }).ToList()
            })
            .FirstOrDefaultAsync();

        if (group == null)
        {
            return NotFound();
        }

        return Ok(group);
    }

}

