namespace AdminPanel.Souqcom.Pages;

[PageAuthorize(typeof(CategoryRow))]
public class CategoryPage : Controller
{
    [Route("Souqcom/Category")]
    public ActionResult Index()
    {
        return this.GridPage<CategoryRow>("@/Souqcom/Category/CategoryPage");
    }
}