import { EntityDialog } from '@serenity-is/corelib';
import { CategoryForm, CategoryRow, CategoryService } from '../../ServerTypes/Souqcom';

export class CategoryDialog extends EntityDialog<CategoryRow, any> {
    static override [Symbol.typeInfo] = this.registerClass("AdminPanel.Souqcom.");

    protected override getFormKey() { return CategoryForm.formKey; }
    protected override getRowDefinition() { return CategoryRow; }
    protected override getService() { return CategoryService.baseUrl; }

    protected form = new CategoryForm(this.idPrefix);
}