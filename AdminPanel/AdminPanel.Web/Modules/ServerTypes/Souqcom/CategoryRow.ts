import { fieldsProxy } from "@serenity-is/corelib";

export interface CategoryRow {
    Id?: number;
    Name?: string;
    Photo?: string;
    Description?: string;
}

export abstract class CategoryRow {
    static readonly idProperty = 'Id';
    static readonly nameProperty = 'Name';
    static readonly localTextPrefix = 'Souqcom.Category';
    static readonly deletePermission = 'Category';
    static readonly insertPermission = 'Category';
    static readonly readPermission = 'Category';
    static readonly updatePermission = 'Category';

    static readonly Fields = fieldsProxy<CategoryRow>();
}