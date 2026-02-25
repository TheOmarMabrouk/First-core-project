using MyRow = AdminPanel.Administration.RoleRow;

namespace AdminPanel.Administration;
public interface IRoleDeleteHandler : IDeleteHandler<MyRow> { }

public class RoleDeleteHandler(IRequestContext context)
    : DeleteRequestHandler<MyRow>(context), IRoleDeleteHandler
{
}