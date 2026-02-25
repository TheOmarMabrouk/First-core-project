import { DeleteRequest, DeleteResponse, ListRequest, ListResponse, RetrieveRequest, RetrieveResponse, SaveRequest, SaveResponse, ServiceOptions, serviceRequest } from "@serenity-is/corelib";
import { ProductsRow } from "./ProductsRow";

export namespace ProductsService {
    export const baseUrl = 'Souqcom/Products';

    export declare function Create(request: SaveRequest<ProductsRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Update(request: SaveRequest<ProductsRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Delete(request: DeleteRequest, onSuccess?: (response: DeleteResponse) => void, opt?: ServiceOptions<any>): PromiseLike<DeleteResponse>;
    export declare function Retrieve(request: RetrieveRequest, onSuccess?: (response: RetrieveResponse<ProductsRow>) => void, opt?: ServiceOptions<any>): PromiseLike<RetrieveResponse<ProductsRow>>;
    export declare function List(request: ListRequest, onSuccess?: (response: ListResponse<ProductsRow>) => void, opt?: ServiceOptions<any>): PromiseLike<ListResponse<ProductsRow>>;

    export const Methods = {
        Create: "Souqcom/Products/Create",
        Update: "Souqcom/Products/Update",
        Delete: "Souqcom/Products/Delete",
        Retrieve: "Souqcom/Products/Retrieve",
        List: "Souqcom/Products/List"
    } as const;

    [
        'Create',
        'Update',
        'Delete',
        'Retrieve',
        'List'
    ].forEach(x => {
        (<any>ProductsService)[x] = function (r, s, o) {
            return serviceRequest(baseUrl + '/' + x, r, s, o);
        };
    });
}