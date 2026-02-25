import { EntityDialog } from '@serenity-is/corelib';
import { CartForm, CartRow, CartService } from '../../ServerTypes/Souqcom';

export class CartDialog extends EntityDialog<CartRow, any> {
    static override [Symbol.typeInfo] = this.registerClass("AdminPanel.Souqcom.");

    protected override getFormKey() { return CartForm.formKey; }
    protected override getRowDefinition() { return CartRow; }
    protected override getService() { return CartService.baseUrl; }

    protected form = new CartForm(this.idPrefix);
}