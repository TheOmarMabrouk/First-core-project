using MyRow = AdminPanel.Souqcom.ProductsRow;

namespace AdminPanel.Souqcom;

public interface IProductsRetrieveHandler : IRetrieveHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>> { }

public class ProductsRetrieveHandler(IRequestContext context) :
    RetrieveRequestHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>>(context),
    IProductsRetrieveHandler
{
}