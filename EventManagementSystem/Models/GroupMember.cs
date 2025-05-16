namespace EventManagementSystem.Models;

public class GroupMember
{
    public int PersonId { get; set; }
    public Person? Person { get; set; }

    public int GroupId { get; set; }
    public Group? Group { get; set; }
}