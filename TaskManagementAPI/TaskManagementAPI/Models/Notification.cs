using System.Text.Json.Serialization;

namespace TaskManagementAPI.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public bool IsChecked { get; set; } = false;
        [JsonIgnore]
        public virtual User User { get; set; }
    }
}
