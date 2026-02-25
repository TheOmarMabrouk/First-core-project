import { EntityDialog } from '@serenity-is/corelib';
import { ProductImagesForm, ProductImagesRow, ProductImagesService } from '../../ServerTypes/Souqcom';

export class ProductImagesDialog extends EntityDialog<ProductImagesRow, any> {
    static override [Symbol.typeInfo] = this.registerClass("AdminPanel.Souqcom.");

    protected override getFormKey() { return ProductImagesForm.formKey; }
    protected override getRowDefinition() { return ProductImagesRow; }
    protected override getService() { return ProductImagesService.baseUrl; }

    protected form = new ProductImagesForm(this.idPrefix);
}