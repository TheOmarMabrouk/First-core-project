using MyRow = AdminPanel.Souqcom.CartRow;

namespace AdminPanel.Souqcom;

public interface ICartSaveHandler : ISaveHandler<MyRow, SaveRequest<MyRow>, SaveResponse> { }

public class CartSaveHandler(IRequestContext context) :
    SaveRequestHandler<MyRow, SaveRequest<MyRow>, SaveResponse>(context),
    ICartSaveHandler
{
}