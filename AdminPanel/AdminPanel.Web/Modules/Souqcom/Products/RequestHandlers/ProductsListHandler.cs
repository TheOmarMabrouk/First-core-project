using MyRow = AdminPanel.Souqcom.ProductsRow;

namespace AdminPanel.Souqcom;

public interface IProductsListHandler : IListHandler<MyRow, ListRequest, ListResponse<MyRow>> { }

public class ProductsListHandler(IRequestContext context) :
    ListRequestHandler<MyRow, ListRequest, ListResponse<MyRow>>(context),
    IProductsListHandler
{
}