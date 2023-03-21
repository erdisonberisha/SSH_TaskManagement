using System.Text.Json.Serialization;

namespace TaskManagementAPI.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string? MediaURL { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public int TaskId { get; set; }
        [JsonIgnore]
        public TaskEntity Task { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
}