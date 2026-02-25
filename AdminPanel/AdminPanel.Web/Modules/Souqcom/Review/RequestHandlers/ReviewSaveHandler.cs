using MyRow = AdminPanel.Souqcom.ReviewRow;

namespace AdminPanel.Souqcom;

public interface IReviewSaveHandler : ISaveHandler<MyRow, SaveRequest<MyRow>, SaveResponse> { }

public class ReviewSaveHandler(IRequestContext context) :
    SaveRequestHandler<MyRow, SaveRequest<MyRow>, SaveResponse>(context),
    IReviewSaveHandler
{
}