using System.ComponentModel.DataAnnotations;

namespace EventManagementSystem.Models;

public class Person
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;

    // Navigation property
    public List<GroupMember> GroupMembers { get; set; } = new List<GroupMember>();
}