using System.Text.Json.Serialization;

namespace TaskManagementAPI.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
}
