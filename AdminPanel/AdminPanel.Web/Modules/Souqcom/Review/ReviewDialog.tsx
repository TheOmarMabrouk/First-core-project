import { EntityDialog } from '@serenity-is/corelib';
import { ReviewForm, ReviewRow, ReviewService } from '../../ServerTypes/Souqcom';

export class ReviewDialog extends EntityDialog<ReviewRow, any> {
    static override [Symbol.typeInfo] = this.registerClass("AdminPanel.Souqcom.");

    protected override getFormKey() { return ReviewForm.formKey; }
    protected override getRowDefinition() { return ReviewRow; }
    protected override getService() { return ReviewService.baseUrl; }

    protected form = new ReviewForm(this.idPrefix);
}