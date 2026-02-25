using MyRow = AdminPanel.Souqcom.ReviewRow;

namespace AdminPanel.Souqcom;

public interface IReviewListHandler : IListHandler<MyRow, ListRequest, ListResponse<MyRow>> { }

public class ReviewListHandler(IRequestContext context) :
    ListRequestHandler<MyRow, ListRequest, ListResponse<MyRow>>(context),
    IReviewListHandler
{
}