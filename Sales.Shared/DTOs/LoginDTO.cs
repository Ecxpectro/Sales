using System.ComponentModel.DataAnnotations;

namespace Sales.Shared.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "The field {0} is required.")]
        [EmailAddress(ErrorMessage = "You must enter a valid email.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "The field {0} is required.")]
        [MinLength(6, ErrorMessage = "The field {0} must have at least {1} characters.")]
        public string Password { get; set; } = null!;
    }

}
