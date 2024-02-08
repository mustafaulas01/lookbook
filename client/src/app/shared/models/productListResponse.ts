import { Product } from "./products";

export interface ProductListResponse {
    data:Product[],
    pageNumber:number,
    pageSize:number,
    totalCount:number
}