namespace AdminPanel.Souqcom.Forms;

[FormScript("Souqcom.Cart")]
[BasedOnRow(typeof(CartRow), CheckNames = true)]
public class CartForm
{
    public string UserId { get; set; }
    public int ProductId { get; set; }
    public int Qty { get; set; }
}