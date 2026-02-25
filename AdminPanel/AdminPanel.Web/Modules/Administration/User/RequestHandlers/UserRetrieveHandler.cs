using MyRow = AdminPanel.Administration.UserRow;

namespace AdminPanel.Administration;
public interface IUserRetrieveHandler : IRetrieveHandler<MyRow> { }

public class UserRetrieveHandler(IRequestContext context)
    : RetrieveRequestHandler<MyRow>(context), IUserRetrieveHandler
{
}