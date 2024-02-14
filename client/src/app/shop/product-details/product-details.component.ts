import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/shared/models/products';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {

  product?:Product;
 constructor(private shopService:ShopService,private activatedRoute:ActivatedRoute,private bvService:BreadcrumbService){}

  ngOnInit(): void {
    this.loadProduct()
  }

  loadProduct(){
    const id=this.activatedRoute.snapshot.paramMap.get('id');
   if(id) this.shopService.getProduct(id).subscribe({
    next:response=>{this.product=response;
    this.bvService.set('@productDetails',this.product.productCode)
    },
    error:error=>console.log(error)
   })
  }
}
