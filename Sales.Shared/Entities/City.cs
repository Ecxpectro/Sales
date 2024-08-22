using System.ComponentModel.DataAnnotations;

namespace Sales.Shared.Entities
{
    public class City
    {
        public int Id { get; set; }
        [Display(Name = "City")]
        [Required(ErrorMessage = "The field {0} is required.")]
        [MaxLength(100, ErrorMessage = "The field {0} has a maximum length of {1} characters.")]
        public string Name { get; set; } = null!;
        public int StateId { get; set; }
        public State? State { get; set; }
    }
}
