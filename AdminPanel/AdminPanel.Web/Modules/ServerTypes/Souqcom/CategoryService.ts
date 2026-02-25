import { DeleteRequest, DeleteResponse, ListRequest, ListResponse, RetrieveRequest, RetrieveResponse, SaveRequest, SaveResponse, ServiceOptions, serviceRequest } from "@serenity-is/corelib";
import { CategoryRow } from "./CategoryRow";

export namespace CategoryService {
    export const baseUrl = 'Souqcom/Category';

    export declare function Create(request: SaveRequest<CategoryRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Update(request: SaveRequest<CategoryRow>, onSuccess?: (response: SaveResponse) => void, opt?: ServiceOptions<any>): PromiseLike<SaveResponse>;
    export declare function Delete(request: DeleteRequest, onSuccess?: (response: DeleteResponse) => void, opt?: ServiceOptions<any>): PromiseLike<DeleteResponse>;
    export declare function Retrieve(request: RetrieveRequest, onSuccess?: (response: RetrieveResponse<CategoryRow>) => void, opt?: ServiceOptions<any>): PromiseLike<RetrieveResponse<CategoryRow>>;
    export declare function List(request: ListRequest, onSuccess?: (response: ListResponse<CategoryRow>) => void, opt?: ServiceOptions<any>): PromiseLike<ListResponse<CategoryRow>>;

    export const Methods = {
        Create: "Souqcom/Category/Create",
        Update: "Souqcom/Category/Update",
        Delete: "Souqcom/Category/Delete",
        Retrieve: "Souqcom/Category/Retrieve",
        List: "Souqcom/Category/List"
    } as const;

    [
        'Create',
        'Update',
        'Delete',
        'Retrieve',
        'List'
    ].forEach(x => {
        (<any>CategoryService)[x] = function (r, s, o) {
            return serviceRequest(baseUrl + '/' + x, r, s, o);
        };
    });
}