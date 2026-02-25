using MyRow = AdminPanel.Administration.LanguageRow;

namespace AdminPanel.Administration;
public interface ILanguageSaveHandler : ISaveHandler<MyRow> { }

public class LanguageSaveHandler(IRequestContext context)
    : SaveRequestHandler<MyRow>(context), ILanguageSaveHandler
{
}