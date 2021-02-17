using System.ComponentModel.DataAnnotations;

namespace BlogApi.Models
{
    public class RegisterUserModel
    {
        [Required]
        public string UserFullName { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
    }
}