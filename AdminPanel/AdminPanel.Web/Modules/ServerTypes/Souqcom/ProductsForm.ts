import { DecimalEditor, initFormType, PrefixedContext, ServiceLookupEditor, StringEditor } from "@serenity-is/corelib";

export interface ProductsForm {
    Name: StringEditor;
    Discription: StringEditor;
    Price: DecimalEditor;
    Catid: ServiceLookupEditor;
    Photo: StringEditor;
    Type: StringEditor;
    SupplierName: StringEditor;
    EntryDate: StringEditor;
    ReviewUrl: StringEditor;
}

export class ProductsForm extends PrefixedContext {
    static readonly formKey = 'Souqcom.Products';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!ProductsForm.init) {
            ProductsForm.init = true;

            var w0 = StringEditor;
            var w1 = DecimalEditor;
            var w2 = ServiceLookupEditor;

            initFormType(ProductsForm, [
                'Name', w0,
                'Discription', w0,
                'Price', w1,
                'Catid', w2,
                'Photo', w0,
                'Type', w0,
                'SupplierName', w0,
                'EntryDate', w0,
                'ReviewUrl', w0
            ]);
        }
    }
}