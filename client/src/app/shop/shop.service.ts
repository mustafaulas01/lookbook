import { Injectable } from '@angular/core';
import {HttpClient,HttpParams} from '@angular/common/http'
import { Product } from '../shared/models/products';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl="https://localhost:5001/api/"
  
  constructor(private http:HttpClient) { }

  getProducts()
  {
    return this.http.get<Product[]>(this.baseUrl+'products');
  }
}
