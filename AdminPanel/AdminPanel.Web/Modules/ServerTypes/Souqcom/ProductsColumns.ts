import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { ProductsRow } from "./ProductsRow";

export interface ProductsColumns {
    Id: Column<ProductsRow>;
    Name: Column<ProductsRow>;
    Discription: Column<ProductsRow>;
    Price: Column<ProductsRow>;
    CatidName: Column<ProductsRow>;
    Photo: Column<ProductsRow>;
    Type: Column<ProductsRow>;
    SupplierName: Column<ProductsRow>;
    EntryDate: Column<ProductsRow>;
    ReviewUrl: Column<ProductsRow>;
}

export class ProductsColumns extends ColumnsBase<ProductsRow> {
    static readonly columnsKey = 'Souqcom.Products';
    static readonly Fields = fieldsProxy<ProductsColumns>();
}