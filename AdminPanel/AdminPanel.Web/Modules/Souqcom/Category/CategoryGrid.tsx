import { EntityGrid } from '@serenity-is/corelib';
import { CategoryColumns, CategoryRow, CategoryService } from '../../ServerTypes/Souqcom';
import { CategoryDialog } from './CategoryDialog';

export class CategoryGrid extends EntityGrid<CategoryRow> {
    static override [Symbol.typeInfo] = this.registerClass("AdminPanel.Souqcom.");

    protected override getColumnsKey() { return CategoryColumns.columnsKey; }
    protected override getDialogType() { return CategoryDialog; }
    protected override getRowDefinition() { return CategoryRow; }
    protected override getService() { return CategoryService.baseUrl; }
}