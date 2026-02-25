using MyRow = AdminPanel.Souqcom.ProductImagesRow;

namespace AdminPanel.Souqcom;

public interface IProductImagesRetrieveHandler : IRetrieveHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>> { }

public class ProductImagesRetrieveHandler(IRequestContext context) :
    RetrieveRequestHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>>(context),
    IProductImagesRetrieveHandler
{
}