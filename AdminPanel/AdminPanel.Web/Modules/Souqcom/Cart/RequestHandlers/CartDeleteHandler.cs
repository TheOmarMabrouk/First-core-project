using MyRow = AdminPanel.Souqcom.CartRow;

namespace AdminPanel.Souqcom;

public interface ICartDeleteHandler : IDeleteHandler<MyRow, DeleteRequest, DeleteResponse> { }

public class CartDeleteHandler(IRequestContext context) :
    DeleteRequestHandler<MyRow, DeleteRequest, DeleteResponse>(context),
    ICartDeleteHandler
{
}