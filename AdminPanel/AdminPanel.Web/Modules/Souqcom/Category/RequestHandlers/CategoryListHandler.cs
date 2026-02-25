using MyRow = AdminPanel.Souqcom.CategoryRow;

namespace AdminPanel.Souqcom;

public interface ICategoryListHandler : IListHandler<MyRow, ListRequest, ListResponse<MyRow>> { }

public class CategoryListHandler(IRequestContext context) :
    ListRequestHandler<MyRow, ListRequest, ListResponse<MyRow>>(context),
    ICategoryListHandler
{
}