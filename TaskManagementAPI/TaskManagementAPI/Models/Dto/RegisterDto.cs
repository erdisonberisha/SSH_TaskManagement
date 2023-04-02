using System.ComponentModel.DataAnnotations;

namespace TaskManagementAPI.Models.Dto
{
    public class RegisterDto
    {
        [MaxLength(40)]
        public string Name { get; set; }

        [MaxLength(120)]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(20)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
