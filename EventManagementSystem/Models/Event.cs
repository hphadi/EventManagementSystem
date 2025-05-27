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
}

public class EventWithGroupsDto : EventBase
{
    public List<SimpleGroupDto> Groups = [];
}

public class SimpleGroupDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
}



