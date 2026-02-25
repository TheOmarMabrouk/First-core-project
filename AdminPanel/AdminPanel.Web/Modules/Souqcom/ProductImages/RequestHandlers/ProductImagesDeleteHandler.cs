using MyRow = AdminPanel.Souqcom.ProductImagesRow;

namespace AdminPanel.Souqcom;

public interface IProductImagesDeleteHandler : IDeleteHandler<MyRow, DeleteRequest, DeleteResponse> { }

public class ProductImagesDeleteHandler(IRequestContext context) :
    DeleteRequestHandler<MyRow, DeleteRequest, DeleteResponse>(context),
    IProductImagesDeleteHandler
{
}