namespace First_core_project.Models
{
    
        public class ProductVm // ده الـ ViewModel اللي هتبعته للـ View
        {
            public string Name { get; set; }
            // ... باقي الخصائص
            public List<IFormFile> Images { get; set; } // هنا السر
        }
    
}
