namespace AdminPanel.Souqcom;

[ConnectionKey("souq"), Module("Souqcom"), TableName("Review")]
[DisplayName("Review"), InstanceName("Review")]
[ReadPermission("Review")]
[ModifyPermission("Review")]
[ServiceLookupPermission("Review")]
public sealed class ReviewRow : Row<ReviewRow.RowFields>, IIdRow, INameRow
{
    [DisplayName("Id"), Identity, IdProperty]
    public int? Id { get => fields.Id[this]; set => fields.Id[this] = value; }

    [DisplayName("Name"), QuickSearch, NameProperty]
    public string Name { get => fields.Name[this]; set => fields.Name[this] = value; }

    [DisplayName("Email"), Size(100)]
    public string Email { get => fields.Email[this]; set => fields.Email[this] = value; }

    [DisplayName("Subject")]
    public string Subject { get => fields.Subject[this]; set => fields.Subject[this] = value; }

    [DisplayName("Description"), Column("description")]
    public string Description { get => fields.Description[this]; set => fields.Description[this] = value; }

    public class RowFields : RowFieldsBase
    {
        public Int32Field Id;
        public StringField Name;
        public StringField Email;
        public StringField Subject;
        public StringField Description;

    }
}