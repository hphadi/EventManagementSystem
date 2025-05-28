namespace EventManagementSystem.Models;

public class EventBase
{
public int Id { get; set; }
public string Title { get; set; } = string.Empty;
public string Description { get; set; } = string.Empty;
public DateTime StartDate { get; set; }
public DateTime EndDate { get; set; }
public DateTime CreatedAt { get; set; }
public string Location { get; set; } = string.Empty;
}

public class Event : EventBase
{
    public List<EventGroup> EventGroups = [];
    public List<EventPerson> EventPersons { get; set; } = new();
}

public class EventWithGroupsDto : EventBase
{
    public List<SimpleGroupDto> Groups { get; set; } = new();
}

public class SimpleGroupDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
}

public class NewEventDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public string Location { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public List<int>? GroupIds { get; set; }
    public List<int>? PersonIds { get; set; }
}



