using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class Login
    {

        public int Id{ get; set; }

        [Required(ErrorMessage = "Username is required")]
        [MinLength(3, ErrorMessage = "Minimum 3 characters required")]
        [MaxLength(20, ErrorMessage = "Maximum 20 characters allowed")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Minimum 6 characters required")]
        public string Password { get; set; }
    }
}

