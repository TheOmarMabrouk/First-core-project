namespace AdminPanel.Souqcom.Forms;

[FormScript("Souqcom.ProductImages")]
[BasedOnRow(typeof(ProductImagesRow), CheckNames = true)]
public class ProductImagesForm
{
    public int ProductId { get; set; }
    public string Image { get; set; }
}