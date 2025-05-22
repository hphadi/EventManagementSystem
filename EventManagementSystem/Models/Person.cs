using System.ComponentModel.DataAnnotations;

namespace EventManagementSystem.Models;

public class Person
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Username { get; set; } = string.Empty;
    [Required]
    [StringLength(100)]
    public string Password { get; set; } = string.Empty;

    // Navigation property
    //public List<GroupMember> GroupMembers { get; set; } = new List<GroupMember>();
}

public class PersonDto
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Username { get; set; } = string.Empty;
    [Required]
    [StringLength(100)]
    public string Password { get; set; } = string.Empty;
}
