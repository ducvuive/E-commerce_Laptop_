using System.ComponentModel.DataAnnotations;

namespace ShareView.DTO
{
    public class InvoiceDTO
    {
        [Key]
        public int InvoiceId { get; set; }
        public DateTime? DateReceived { get; set; }
        public string? Receiver { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public long? Total { get; set; }
        public int? Status { get; set; }
        public UserIdentityDTO? Customer { get; set; }
        //public virtual UserIdentityDTO? MaKhacHangId { get; set; }
        //public virtual List<CTHD_DTO> CTHD { get; set; } = new List<CTHD_DTO>();
    }
}
