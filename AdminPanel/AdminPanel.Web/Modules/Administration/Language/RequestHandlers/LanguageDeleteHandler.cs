using MyRow = AdminPanel.Administration.LanguageRow;

namespace AdminPanel.Administration;
public interface ILanguageDeleteHandler : IDeleteHandler<MyRow> { }

public class LanguageDeleteHandler(IRequestContext context)
    : DeleteRequestHandler<MyRow>(context), ILanguageDeleteHandler
{
}