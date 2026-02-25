using MyRow = AdminPanel.Administration.RoleRow;

namespace AdminPanel.Administration;
public interface IRoleListHandler : IListHandler<MyRow> { }

public class RoleListHandler(IRequestContext context)
    : ListRequestHandler<MyRow>(context), IRoleListHandler
{
}