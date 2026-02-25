using MyRow = AdminPanel.Souqcom.ReviewRow;

namespace AdminPanel.Souqcom;

public interface IReviewRetrieveHandler : IRetrieveHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>> { }

public class ReviewRetrieveHandler(IRequestContext context) :
    RetrieveRequestHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>>(context),
    IReviewRetrieveHandler
{
}