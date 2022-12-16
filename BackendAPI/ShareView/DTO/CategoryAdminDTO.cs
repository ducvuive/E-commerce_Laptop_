using System.ComponentModel.DataAnnotations;

namespace ShareView.DTO
{
    public class CategoryAdminDTO
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? isDisabled { get; set; }
    }
}
