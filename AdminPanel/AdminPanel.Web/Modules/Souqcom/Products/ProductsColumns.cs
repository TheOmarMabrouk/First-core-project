namespace AdminPanel.Souqcom.Columns;

[ColumnsScript("Souqcom.Products")]
[BasedOnRow(typeof(ProductsRow), CheckNames = true)]
public class ProductsColumns
{
    [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
    public int Id { get; set; }
    [EditLink]
    public string Name { get; set; }
    public string Discription { get; set; }
    public decimal Price { get; set; }
    public string CatidName { get; set; }
    public string Photo { get; set; }
    public string Type { get; set; }
    public string SupplierName { get; set; }
    public DateOnly EntryDate { get; set; }
    public string ReviewUrl { get; set; }
}