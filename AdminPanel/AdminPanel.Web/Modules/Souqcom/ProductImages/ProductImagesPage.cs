namespace AdminPanel.Souqcom.Pages;

[PageAuthorize(typeof(ProductImagesRow))]
public class ProductImagesPage : Controller
{
    [Route("Souqcom/ProductImages")]
    public ActionResult Index()
    {
        return this.GridPage<ProductImagesRow>("@/Souqcom/ProductImages/ProductImagesPage");
    }
}