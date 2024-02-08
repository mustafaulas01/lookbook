import { Injectable } from '@angular/core';
import {HttpClient,HttpParams} from '@angular/common/http'
import { Product } from '../shared/models/products';
import { ProductListResponse } from '../shared/models/productListResponse';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl="https://localhost:5001/api/"
  
  constructor(private http:HttpClient) { }

  getProducts(gender?:string,category?:string,sort?:string,pageNumber:number=1,pageSize:number=12,search?:string,isAscending?:boolean)
  {
    let myparams=new HttpParams();

    if(gender)
    myparams=myparams.append("gender",gender)
    if(category)
    myparams=myparams.append("category",category);
    if(sort)
    myparams=myparams.append("sort",sort);

    myparams=myparams.append("pageNumber",pageNumber);
    myparams=myparams.append("pageSize",pageSize);
    if(search)
    myparams=myparams.append("search",search)

    if(isAscending)
    myparams=myparams.append("isascending",isAscending);

  

    return this.http.get<ProductListResponse>(this.baseUrl+'products',{params:myparams});
  }
}
