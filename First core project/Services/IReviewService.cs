using First_core_project.Models;

namespace First_core_project.Services
{
    public interface IReviewService
    {
        Task AddReviewAsync(ReviewVm model);
    }
}