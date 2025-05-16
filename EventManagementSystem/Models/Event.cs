namespace EventManagementSystem.Models
{
    public class Event
    {
        public int Id { get; set; }

        public DateTime created_at { get; set; }

        public string Title { get; set; } = string.Empty;
    }
}
