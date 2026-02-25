using MyRow = AdminPanel.Souqcom.CategoryRow;

namespace AdminPanel.Souqcom;

public interface ICategorySaveHandler : ISaveHandler<MyRow, SaveRequest<MyRow>, SaveResponse> { }

public class CategorySaveHandler(IRequestContext context) :
    SaveRequestHandler<MyRow, SaveRequest<MyRow>, SaveResponse>(context),
    ICategorySaveHandler
{
}