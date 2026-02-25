import { EntityDialog } from '@serenity-is/corelib';
import { ProductsForm, ProductsRow, ProductsService } from '../../ServerTypes/Souqcom';

export class ProductsDialog extends EntityDialog<ProductsRow, any> {
    static override [Symbol.typeInfo] = this.registerClass("AdminPanel.Souqcom.");

    protected override getFormKey() { return ProductsForm.formKey; }
    protected override getRowDefinition() { return ProductsRow; }
    protected override getService() { return ProductsService.baseUrl; }

    protected form = new ProductsForm(this.idPrefix);
}