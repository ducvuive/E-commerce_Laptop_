using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BackendAPI.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        [MaxLength(100)]
        public string? Description { get; set; }

        public int? isDisabled { get; set; }

        public virtual List<Product> Products { get; set; } = new List<Product>();
    }
}
