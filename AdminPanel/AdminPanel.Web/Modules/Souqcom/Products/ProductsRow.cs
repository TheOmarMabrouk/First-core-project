namespace AdminPanel.Souqcom;

[ConnectionKey("souq"), Module("Souqcom"), TableName("Products")]
[DisplayName("Products"), InstanceName("Products")]
[ReadPermission("Products")]
[ModifyPermission("Products")]
[ServiceLookupPermission("Products")]
public sealed class ProductsRow : Row<ProductsRow.RowFields>, IIdRow, INameRow
{
    const string jCatid = nameof(jCatid);

    [DisplayName("Id"), Identity, IdProperty]
    public int? Id { get => fields.Id[this]; set => fields.Id[this] = value; }

    [DisplayName("Name"), QuickSearch, NameProperty]
    public string Name { get => fields.Name[this]; set => fields.Name[this] = value; }

    [DisplayName("Discription")]
    public string Discription { get => fields.Discription[this]; set => fields.Discription[this] = value; }

    [DisplayName("Price"), Size(18)]
    public decimal? Price { get => fields.Price[this]; set => fields.Price[this] = value; }

    [DisplayName("Catid"), ForeignKey(typeof(CategoryRow)), LeftJoin(jCatid), TextualField(nameof(CatidName))]
    [ServiceLookupEditor(typeof(CategoryRow), Service = "Souqcom/Category/List")]
    public int? Catid { get => fields.Catid[this]; set => fields.Catid[this] = value; }

    [DisplayName("Photo")]
    public string Photo { get => fields.Photo[this]; set => fields.Photo[this] = value; }

    [DisplayName("Type")]
    public string Type { get => fields.Type[this]; set => fields.Type[this] = value; }

    [DisplayName("Supplier Name"), Size(50)]
    public string SupplierName { get => fields.SupplierName[this]; set => fields.SupplierName[this] = value; }

    [DisplayName("Entry Date")]
    public DateOnly? EntryDate { get => fields.EntryDate[this]; set => fields.EntryDate[this] = value; }

    [DisplayName("Review Url")]
    public string ReviewUrl { get => fields.ReviewUrl[this]; set => fields.ReviewUrl[this] = value; }

    [DisplayName("Catid Name"), Origin(jCatid, nameof(CategoryRow.Name))]
    public string CatidName { get => fields.CatidName[this]; set => fields.CatidName[this] = value; }

    public class RowFields : RowFieldsBase
    {
        public Int32Field Id;
        public StringField Name;
        public StringField Discription;
        public DecimalField Price;
        public Int32Field Catid;
        public StringField Photo;
        public StringField Type;
        public StringField SupplierName;
        public DateOnlyField EntryDate;
        public StringField ReviewUrl;

        public StringField CatidName;
    }
}