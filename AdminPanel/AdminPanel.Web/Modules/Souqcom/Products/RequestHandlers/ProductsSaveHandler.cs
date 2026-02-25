using MyRow = AdminPanel.Souqcom.ProductsRow;

namespace AdminPanel.Souqcom;

public interface IProductsSaveHandler : ISaveHandler<MyRow, SaveRequest<MyRow>, SaveResponse> { }

public class ProductsSaveHandler(IRequestContext context) :
    SaveRequestHandler<MyRow, SaveRequest<MyRow>, SaveResponse>(context),
    IProductsSaveHandler
{
}