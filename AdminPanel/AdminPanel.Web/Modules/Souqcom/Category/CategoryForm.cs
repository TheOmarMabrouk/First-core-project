namespace AdminPanel.Souqcom.Forms;

[FormScript("Souqcom.Category")]
[BasedOnRow(typeof(CategoryRow), CheckNames = true)]
public class CategoryForm
{
    public string Name { get; set; }
    public string Photo { get; set; }
    public string Description { get; set; }
}