using BackendAPI.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendAPI.Models
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

        [ForeignKey("CustomerId")]
        public virtual UserIdentity Customer { get; set; }
    }
}
