import { EntityGrid } from '@serenity-is/corelib';
import { ReviewColumns, ReviewRow, ReviewService } from '../../ServerTypes/Souqcom';
import { ReviewDialog } from './ReviewDialog';

export class ReviewGrid extends EntityGrid<ReviewRow> {
    static override [Symbol.typeInfo] = this.registerClass("AdminPanel.Souqcom.");

    protected override getColumnsKey() { return ReviewColumns.columnsKey; }
    protected override getDialogType() { return ReviewDialog; }
    protected override getRowDefinition() { return ReviewRow; }
    protected override getService() { return ReviewService.baseUrl; }
}