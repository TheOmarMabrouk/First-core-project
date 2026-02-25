namespace AdminPanel.Souqcom;

[ConnectionKey("souq"), Module("Souqcom"), TableName("Category")]
[DisplayName("Category"), InstanceName("Category")]
[ReadPermission("Category")]
[ModifyPermission("Category")]
[ServiceLookupPermission("Category")]
public sealed class CategoryRow : Row<CategoryRow.RowFields>, IIdRow, INameRow
{
    [DisplayName("Id"), Identity, IdProperty]
    public int? Id { get => fields.Id[this]; set => fields.Id[this] = value; }

    [DisplayName("Name"), QuickSearch, NameProperty]
    public string Name { get => fields.Name[this]; set => fields.Name[this] = value; }

    [DisplayName("Photo")]
    public string Photo { get => fields.Photo[this]; set => fields.Photo[this] = value; }

    [DisplayName("Description"), Column("description"), Size(50)]
    public string Description { get => fields.Description[this]; set => fields.Description[this] = value; }

    public class RowFields : RowFieldsBase
    {
        public Int32Field Id;
        public StringField Name;
        public StringField Photo;
        public StringField Description;

    }
}