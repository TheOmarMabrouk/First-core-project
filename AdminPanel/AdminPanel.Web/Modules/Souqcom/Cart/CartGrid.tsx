import { EntityGrid } from '@serenity-is/corelib';
import { CartColumns, CartRow, CartService } from '../../ServerTypes/Souqcom';
import { CartDialog } from './CartDialog';

export class CartGrid extends EntityGrid<CartRow> {
    static override [Symbol.typeInfo] = this.registerClass("AdminPanel.Souqcom.");

    protected override getColumnsKey() { return CartColumns.columnsKey; }
    protected override getDialogType() { return CartDialog; }
    protected override getRowDefinition() { return CartRow; }
    protected override getService() { return CartService.baseUrl; }
}