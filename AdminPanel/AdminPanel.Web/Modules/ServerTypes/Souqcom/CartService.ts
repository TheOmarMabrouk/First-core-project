import { DeleteRequest, DeleteResponse, ListRequest, ListResponse, RetrieveRequest, RetrieveResponse, SaveRequest, SaveResponse, ServiceOptions, serviceRequest } from "@serenity-is/corelib";
import { CartRow } from "./CartRow";

export namespace CartService {
    export const baseUrl = 'Souqcom/Cart';

    export declare function Create(request: SaveRequest<CartRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Update(request: SaveRequest<CartRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Delete(request: DeleteRequest, onSuccess?: (response: DeleteResponse) => void, opt?: ServiceOptions<any>): PromiseLike<DeleteResponse>;
    export declare function Retrieve(request: RetrieveRequest, onSuccess?: (response: RetrieveResponse<CartRow>) => void, opt?: ServiceOptions<any>): PromiseLike<RetrieveResponse<CartRow>>;
    export declare function List(request: ListRequest, onSuccess?: (response: ListResponse<CartRow>) => void, opt?: ServiceOptions<any>): PromiseLike<ListResponse<CartRow>>;

    export const Methods = {
        Create: "Souqcom/Cart/Create",
        Update: "Souqcom/Cart/Update",
        Delete: "Souqcom/Cart/Delete",
        Retrieve: "Souqcom/Cart/Retrieve",
        List: "Souqcom/Cart/List"
    } as const;

    [
        'Create',
        'Update',
        'Delete',
        'Retrieve',
        'List'
    ].forEach(x => {
        (<any>CartService)[x] = function (r, s, o) {
            return serviceRequest(baseUrl + '/' + x, r, s, o);
        };
    });
}