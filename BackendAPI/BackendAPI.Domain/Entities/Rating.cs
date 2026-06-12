using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendAPI.Domain.Entities
{
    public partial class Rating
    {
        [Key]
        public int RatingID { get; set; }
        public int? Rate { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Comments { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        public string? CustomerId { get; set; }
    }
}
