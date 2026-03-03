using First_core_project.DTOs;

namespace First_core_project.Services
{
    public interface IOrderService
    {
        Task<CheckoutResultDto> CheckoutAsync(string userId);
        Task MarkOrderAsPaidAsync(int orderId);
    }
}
