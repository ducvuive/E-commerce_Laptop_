namespace ShareView.DTO
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public int ScreenId { get; set; }

        public int ProcessorId { get; set; }
        public int RamId { get; set; }
        //public int CongKetNoiId { get; set; }
        public int CategoryId { get; set; }
        public string? NameProduct { get; set; }
        public int? Quantity { get; set; }
        public long? Price { get; set; }
        public string? Image { get; set; }
        public float? Rating { get; set; }
        public DateTime? PublishedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public List<RatingDTO> Ratings { get; set; }
    }
}
