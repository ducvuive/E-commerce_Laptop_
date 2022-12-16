namespace ShareView.DTO
{
    public class ProductPagingDTO
    {
        public List<ProductDTO> Products { get; set; }
        public int TotalItem { get; set; }
        public int Page { get; set; }
        public int LastPage { get; set; }
    }
}
