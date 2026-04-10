namespace First_core_project.DTOs.API
{
    public class ApiProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string MainImage { get; set; }
        public List<string> Images { get; set; }
    }
}