using First_core_project.Models;

namespace First_core_project.Services
{
    public interface IHomeService
    {
        Task<IndexVm> GetHomeDataAsync();
    }
}