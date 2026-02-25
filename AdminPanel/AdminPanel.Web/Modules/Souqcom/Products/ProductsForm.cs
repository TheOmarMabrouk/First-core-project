namespace AdminPanel.Souqcom.Forms;

[FormScript("Souqcom.Products")]
[BasedOnRow(typeof(ProductsRow), CheckNames = true)]
public class ProductsForm
{
    public string Name { get; set; }
    public string Discription { get; set; }
    public decimal Price { get; set; }
    public int Catid { get; set; }
    public string Photo { get; set; }
    public string Type { get; set; }
    public string SupplierName { get; set; }
    public DateOnly EntryDate { get; set; }
    public string ReviewUrl { get; set; }
}