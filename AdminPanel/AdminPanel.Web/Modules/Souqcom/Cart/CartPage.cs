namespace AdminPanel.Souqcom.Pages;

[PageAuthorize(typeof(CartRow))]
public class CartPage : Controller
{
    [Route("Souqcom/Cart")]
    public ActionResult Index()
    {
        return this.GridPage<CartRow>("@/Souqcom/Cart/CartPage");
    }
}