import { EntityGrid } from '@serenity-is/corelib';
import { ProductImagesColumns, ProductImagesRow, ProductImagesService } from '../../ServerTypes/Souqcom';
import { ProductImagesDialog } from './ProductImagesDialog';

export class ProductImagesGrid extends EntityGrid<ProductImagesRow> {
    static override [Symbol.typeInfo] = this.registerClass("AdminPanel.Souqcom.");

    protected override getColumnsKey() { return ProductImagesColumns.columnsKey; }
    protected override getDialogType() { return ProductImagesDialog; }
    protected override getRowDefinition() { return ProductImagesRow; }
    protected override getService() { return ProductImagesService.baseUrl; }
}