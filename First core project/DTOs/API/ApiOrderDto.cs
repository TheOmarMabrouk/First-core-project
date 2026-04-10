namespace First_core_project.DTOs.API
{
    public class ApiOrderDto
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public int Status { get; set; }
        public string CreatedAt { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }

    public class OrderItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal? Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
    }
}