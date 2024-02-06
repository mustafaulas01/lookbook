import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/shared/models/products';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-shbody',
  templateUrl: './shbody.component.html',
  styleUrls: ['./shbody.component.scss']
})
export class ShbodyComponent implements OnInit {

  sortOptions= [
    {name:'Alphabetical',value:'name'},
    {name:'Price:Low to high',value:'priceAsc'},
    {name:'Price:High to low',value:'priceDesc'}
  ]
  categoryOptions=[
    {name:'Women',value:1},
    {name:'Men',value:2},
    {name:'Kids',value:3},
    {name:'Unisex',value:4},
  ]
  groupOptions=[
   
    {name:'Accessories',value:'Accessories'},
    {name:'Apparel',value:'Apparel'},
    {name:'Bags',value:'Bags'},
    {name:'Shoes',value:'Shoes'}
  ]
  sortSelected='name';
  groupSelected='';
  categorySelected=0;
  products:Product[]=[];

  ngOnInit(): void {
   this.getproducts();
  }
 
  constructor (private shopServices:ShopService){}

  onSortSelected(event:any){
   this.sortSelected=event.target.value;
   //ürün listesi gelecek
   this.getproducts();

  }
  onCategorySelected(categoryId:number){
    this.categorySelected=categoryId;
  }
  onGroupSelected(groupName:string)
  {
    this.groupSelected=groupName;
  }
  getproducts(){
    this.shopServices.getProducts().subscribe({
      next:response=>{ this.products=response;
      
      },
      error:error=>console.log("hata :"+error)
    })
  }
}

