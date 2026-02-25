using MyRow = AdminPanel.Souqcom.CategoryRow;

namespace AdminPanel.Souqcom;

public interface ICategoryRetrieveHandler : IRetrieveHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>> { }

public class CategoryRetrieveHandler(IRequestContext context) :
    RetrieveRequestHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>>(context),
    ICategoryRetrieveHandler
{
}