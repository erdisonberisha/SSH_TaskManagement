using System.ComponentModel.DataAnnotations;

namespace TaskManagementAPI.Models.Dto
{
    public class RegisterDto
    {
        [MaxLength(40)]
        [Required]
        public string Name { get; set; }

        [MaxLength(120)]
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [MaxLength(20)]
        [Required]
        public string Username { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
