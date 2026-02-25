import { fieldsProxy } from "@serenity-is/corelib";

export interface ProductsRow {
    Id?: number;
    Name?: string;
    Discription?: string;
    Price?: number;
    Catid?: number;
    Photo?: string;
    Type?: string;
    SupplierName?: string;
    EntryDate?: string;
    ReviewUrl?: string;
    CatidName?: string;
}

export abstract class ProductsRow {
    static readonly idProperty = 'Id';
    static readonly nameProperty = 'Name';
    static readonly localTextPrefix = 'Souqcom.Products';
    static readonly deletePermission = 'Products';
    static readonly insertPermission = 'Products';
    static readonly readPermission = 'Products';
    static readonly updatePermission = 'Products';

    static readonly Fields = fieldsProxy<ProductsRow>();
}