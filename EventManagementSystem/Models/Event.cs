namespace EventManagementSystem.Models;
public class Event
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public DateTime CreatedAt { get; set; }
    public string Location { get; set; } = string.Empty;
    //public List<EventGroup> EventGroups { get; set; } = new List<EventGroup>();
}
