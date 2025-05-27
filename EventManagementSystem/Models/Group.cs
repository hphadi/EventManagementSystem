using System.Text.Json.Serialization;

namespace EventManagementSystem.Models;

public class GroupBase
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class Group : GroupBase
{
    public List<EventGroup> EventGroups { get; set; } = new();
}

public class GroupWithEventsDto : GroupBase
{
    [JsonPropertyName("events")]
    public List<SimpleEventDto> Events { get; set; } = new();
}

public class SimpleEventDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public string Location { get; set; } = string.Empty;
}
