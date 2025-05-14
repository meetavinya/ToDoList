namespace ToDoList.Models
{
    public class Totask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }

        public int Flag { get; set; } 

        public int UserId { get; set; } 
    }
}
