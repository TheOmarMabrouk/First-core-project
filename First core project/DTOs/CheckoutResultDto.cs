namespace First_core_project.DTOs
{
    public class CheckoutResultDto
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? PaymentUrl { get; set; }
    }
}
