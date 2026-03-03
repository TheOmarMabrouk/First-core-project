namespace First_core_project.Dtos
{
    public class CartUpdateDto
    {
        public bool Success { get; set; }
        public int NewQty { get; set; }
        public decimal ItemTotal { get; set; }
        public decimal CartTotal { get; set; }
    }
}