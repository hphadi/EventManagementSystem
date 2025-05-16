namespace EventManagementSystem.Models
{
    public class Group
    {
        public int Id { get; set; }

        public DateTime created_at { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}
