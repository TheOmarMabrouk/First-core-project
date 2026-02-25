using MyRow = AdminPanel.Souqcom.CategoryRow;

namespace AdminPanel.Souqcom;

public interface ICategoryDeleteHandler : IDeleteHandler<MyRow, DeleteRequest, DeleteResponse> { }

public class CategoryDeleteHandler(IRequestContext context) :
    DeleteRequestHandler<MyRow, DeleteRequest, DeleteResponse>(context),
    ICategoryDeleteHandler
{
}