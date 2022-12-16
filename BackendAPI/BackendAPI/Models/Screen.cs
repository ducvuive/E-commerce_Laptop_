using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BackendAPI.Models
{
    public partial class Screen
    {
        [Key]
        public int ScreenId { get; set; }
        public string? Size { get; set; }
        public virtual List<Product> Product { get; set; } = new List<Product>();
    }
}
