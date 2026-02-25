using MyRow = AdminPanel.Souqcom.CartRow;

namespace AdminPanel.Souqcom;

public interface ICartRetrieveHandler : IRetrieveHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>> { }

public class CartRetrieveHandler(IRequestContext context) :
    RetrieveRequestHandler<MyRow, RetrieveRequest, RetrieveResponse<MyRow>>(context),
    ICartRetrieveHandler
{
}