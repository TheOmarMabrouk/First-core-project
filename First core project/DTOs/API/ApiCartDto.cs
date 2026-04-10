namespace First_core_project.DTOs.API
{
    public class ApiCartDto
    {
        public int TotalItems { get; set; }
        public decimal TotalPrice { get; set; }
        public List<ApiCartItemDto> Items { get; set; } = new List<ApiCartItemDto>();
    }

    public class ApiCartItemDto
    {
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal? Price { get; set; }
        public int Quantity { get; set; }
        public string? MainImage { get; set; }
    }
}