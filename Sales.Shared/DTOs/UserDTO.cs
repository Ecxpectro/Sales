using Sales.Shared.Entities;
using System.ComponentModel.DataAnnotations;

namespace Sales.Shared.DTOs
{
    public class UserDTO : User
    {
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "The field {0} must be between {2} and {1} characters.")]
        public string Password { get; set; } = null!;

        [Compare("Password", ErrorMessage = "The password and confirmation are not the same.")]
        [Display(Name = "Password confirmation")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "The field {0} is required.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "The field {0} must be between {2} and {1} characters.")]
        public string PasswordConfirm { get; set; } = null!;

    }
}
