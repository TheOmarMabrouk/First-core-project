namespace AdminPanel.Souqcom.Pages;

[PageAuthorize(typeof(ProductsRow))]
public class ProductsPage : Controller
{
    [Route("Souqcom/Products")]
    public ActionResult Index()
    {
        return this.GridPage<ProductsRow>("@/Souqcom/Products/ProductsPage");
    }
}