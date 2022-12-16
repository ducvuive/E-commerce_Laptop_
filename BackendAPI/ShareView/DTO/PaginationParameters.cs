namespace ShareView.DTO
{
    public class PaginationParameters
    {
        public int Page { get; set; }
        public int Limit { get; set; }

        public PaginationParameters()
        {
            Page = 1;
            Limit = 10;
        }

        public PaginationParameters(int pageNumber, int pageSize)
        {
            Page = pageNumber < 1 ? 1 : pageNumber;
            Limit = Limit < 1 ? 1 : Limit;
        }
    }
}
