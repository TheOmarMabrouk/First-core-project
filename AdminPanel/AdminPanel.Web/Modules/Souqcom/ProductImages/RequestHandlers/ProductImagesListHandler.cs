using MyRow = AdminPanel.Souqcom.ProductImagesRow;

namespace AdminPanel.Souqcom;

public interface IProductImagesListHandler : IListHandler<MyRow, ListRequest, ListResponse<MyRow>> { }

public class ProductImagesListHandler(IRequestContext context) :
    ListRequestHandler<MyRow, ListRequest, ListResponse<MyRow>>(context),
    IProductImagesListHandler
{
}