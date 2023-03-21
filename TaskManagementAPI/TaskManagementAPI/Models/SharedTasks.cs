namespace TaskManagementAPI.Models
{
    public class SharedTasks
    {
        public int TaskId { get; set; }
        public TaskEntity Task { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public bool Approved { get; set; } = false;
    }
}
