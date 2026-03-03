using First_core_project.Data;
using First_core_project.Models;
using Microsoft.EntityFrameworkCore;

namespace First_core_project.Services
{
    public class HomeService : IHomeService
    {
        private readonly SouqcomContext _db;

        public HomeService(SouqcomContext db)
        {
            _db = db;
        }

        public async Task<IndexVm> GetHomeDataAsync()
        {
            return new IndexVm
            {
                Categories = await _db.Categories.ToListAsync(),
                Products = await _db.Products.ToListAsync(),
                Reviews = await _db.Reviews.ToListAsync(),
                LatestProducts = await _db.Products
                    .OrderByDescending(x => x.EntryDate)
                    .Take(3)
                    .ToListAsync()
            };
        }
    }
}