namespace AdminPanel.Souqcom.Pages;

[PageAuthorize(typeof(ReviewRow))]
public class ReviewPage : Controller
{
    [Route("Souqcom/Review")]
    public ActionResult Index()
    {
        return this.GridPage<ReviewRow>("@/Souqcom/Review/ReviewPage");
    }
}