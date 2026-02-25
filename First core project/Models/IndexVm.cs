namespace First_core_project.Models
{
    public class IndexVm
    {

            public IndexVm()
            {
                Categories = new List<Category>();
                Products = new List<Product>();
                Reviews = new List<Review>();
                LatestProducts = new List<Product>();


            }
        
       
        public List<Category> Categories { get; set; }

        public List<Product> Products { get; set; }

        public List<Review> Reviews { get; set; }

        public List<Product> LatestProducts { get; set; }


    }
}
