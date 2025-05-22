namespace EventManagementSystem.Models
{
    public class EventDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Location { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public List<int> GroupIds { get; set; }
    }
}
