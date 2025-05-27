using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EventManagementSystem.Models;

public class PersonBase
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

public class Person_:PersonBase
{
    public List<EventPerson> EventPersons { get; set; } = new();
}

public class PersonWithEventsDto : PersonBase
{
    [JsonPropertyName("events")]
    public List<SimpleEventDto> Events { get; set; } = new();
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

public class LoginDto
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
