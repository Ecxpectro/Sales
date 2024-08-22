using System.ComponentModel.DataAnnotations;

namespace Sales.Shared.Entities
{
    public class Category
    {
        public int Id { get; set; }
        [Display(Name = "Category")]
        [Required(ErrorMessage = "The field {0} is required.")]
        [MaxLength(100, ErrorMessage = "The field {0} has a maximum length of {1} characters.")]
        public string Name { get; set; } = null!;
    }
}
