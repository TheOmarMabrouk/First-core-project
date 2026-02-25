using MyRow = AdminPanel.Souqcom.ProductImagesRow;

namespace AdminPanel.Souqcom;

public interface IProductImagesSaveHandler : ISaveHandler<MyRow, SaveRequest<MyRow>, SaveResponse> { }

public class ProductImagesSaveHandler(IRequestContext context) :
    SaveRequestHandler<MyRow, SaveRequest<MyRow>, SaveResponse>(context),
    IProductImagesSaveHandler
{
}