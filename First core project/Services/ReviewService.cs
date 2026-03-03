using First_core_project.Data;
using First_core_project.Models;

namespace First_core_project.Services
{
    public class ReviewService : IReviewService
    {
        private readonly SouqcomContext _db;

        public ReviewService(SouqcomContext db)
        {
            _db = db;
        }

        public async Task AddReviewAsync(ReviewVm model)
        {
            var review = new Review
            {
                Name = model.Name,
                Email = model.Email,
                Subject = model.Subject,
                Description = model.Description
            };

            await _db.Reviews.AddAsync(review);
            await _db.SaveChangesAsync();
        }
    }
}