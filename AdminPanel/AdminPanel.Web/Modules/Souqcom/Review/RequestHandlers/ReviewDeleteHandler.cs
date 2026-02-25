using MyRow = AdminPanel.Souqcom.ReviewRow;

namespace AdminPanel.Souqcom;

public interface IReviewDeleteHandler : IDeleteHandler<MyRow, DeleteRequest, DeleteResponse> { }

public class ReviewDeleteHandler(IRequestContext context) :
    DeleteRequestHandler<MyRow, DeleteRequest, DeleteResponse>(context),
    IReviewDeleteHandler
{
}