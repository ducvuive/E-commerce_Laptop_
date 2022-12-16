using System.ComponentModel.DataAnnotations;

namespace ShareView.DTO
{
    public class CategoryDTO
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? isDisabled { get; set; }
    }
}
