using System.Text.Json.Serialization;

namespace TaskManagementAPI.Models
{
    public class SharedTask
    {
        public int TaskId { get; set; }
        public TaskEntity Task { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public bool Approved { get; set; } = false;
    }
}
