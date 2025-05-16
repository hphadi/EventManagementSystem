namespace EventManagementSystem.Models;

public class Group
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public List<EventGroup> EventGroups { get; set; } = new List<EventGroup>();
    public List<GroupMember> GroupMembers { get; set; } = new List<GroupMember>();
}