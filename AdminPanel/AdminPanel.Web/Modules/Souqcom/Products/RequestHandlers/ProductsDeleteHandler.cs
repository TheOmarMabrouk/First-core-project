using MyRow = AdminPanel.Souqcom.ProductsRow;

namespace AdminPanel.Souqcom;

public interface IProductsDeleteHandler : IDeleteHandler<MyRow, DeleteRequest, DeleteResponse> { }

public class ProductsDeleteHandler(IRequestContext context) :
    DeleteRequestHandler<MyRow, DeleteRequest, DeleteResponse>(context),
    IProductsDeleteHandler
{
}