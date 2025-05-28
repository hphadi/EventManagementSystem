namespace EventManagementSystem.Models
{
    public class GroupPerson
    {
        public int GroupId { get; set; }
        public Group Group { get; set; }

        public int PersonId { get; set; }
        public Person_ Person { get; set; }
    }

}
