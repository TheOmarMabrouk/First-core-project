namespace First_core_project.Services
{
    public interface IPaymentService
    {
        Task<string> CreatePaymentAsync(int orderId, decimal total);
    }
}
