﻿using Microsoft.AspNetCore.Identity;
using Sales.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Sales.Shared.Entities
{
    public class User : IdentityUser
    {
        [Display(Name = "Document")]
        [MaxLength(20, ErrorMessage = "The field {0} has a maximum length of {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string Document { get; set; } = null!;

        [Display(Name = "Names")]
        [MaxLength(50, ErrorMessage = "The field {0} has a maximum length of {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Surname")]
        [MaxLength(50, ErrorMessage = "The field {0} has a maximum length of {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string LastName { get; set; } = null!;

        [Display(Name = "Address")]
        [MaxLength(200, ErrorMessage = "The field {0} has a maximum length of {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string Address { get; set; } = null!;

        [Display(Name = "Photo")]
        public string? Photo { get; set; }

        [Display(Name = "User type")]
        public UserType UserType { get; set; }

        public City? City { get; set; }

        [Display(Name = "City")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select one {0}.")]
        public int CityId { get; set; }

        [Display(Name = "User")]
        public string FullName => $"{FirstName} {LastName}";
    }

}
