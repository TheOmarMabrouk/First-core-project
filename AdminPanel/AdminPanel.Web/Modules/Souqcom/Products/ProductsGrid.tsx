import { EntityGrid } from '@serenity-is/corelib';
import { ProductsColumns, ProductsRow, ProductsService } from '../../ServerTypes/Souqcom';
import { ProductsDialog } from './ProductsDialog';

export class ProductsGrid extends EntityGrid<ProductsRow> {
    static override [Symbol.typeInfo] = this.registerClass("AdminPanel.Souqcom.");

    protected override getColumnsKey() { return ProductsColumns.columnsKey; }
    protected override getDialogType() { return ProductsDialog; }
    protected override getRowDefinition() { return ProductsRow; }
    protected override getService() { return ProductsService.baseUrl; }
}