using MyRow = AdminPanel.Souqcom.CartRow;

namespace AdminPanel.Souqcom;

public interface ICartListHandler : IListHandler<MyRow, ListRequest, ListResponse<MyRow>> { }

public class CartListHandler(IRequestContext context) :
    ListRequestHandler<MyRow, ListRequest, ListResponse<MyRow>>(context),
    ICartListHandler
{
}