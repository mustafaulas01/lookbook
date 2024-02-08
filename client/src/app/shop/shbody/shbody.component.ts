import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Product } from 'src/app/shared/models/products';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-shbody',
  templateUrl: './shbody.component.html',
  styleUrls: ['./shbody.component.scss']
})
export class ShbodyComponent implements OnInit {

@ViewChild('search') searchTerm?:ElementRef
  search:string='';
  sortOptions= [
    {name:'Alphabetical',value:'name'},
    {name:'Price:Low to high',value:'priceAsc'},
    {name:'Price:High to low',value:'priceDesc'}
  ]
  categoryOptions=[
    {name:'All',value:'All'},
    {name:'Women',value:'Women'},
    {name:'Men',value:'Men'},
    {name:'Kids',value:'Kids'},
    {name:'Unisex',value:'Unisex'},
  ]
  groupOptions=[
   
    {name:'All',value:'All'},
    {name:'Accessories',value:'Accessories'},
    {name:'Apparel',value:'Apparel'},
    {name:'Bags',value:'Bags'},
    {name:'Shoes',value:'Shoes'}
  ]
  sortSelected='name';
  groupSelected='All';
  categorySelected='All';
  products:Product[]=[];
  totalCount=0;
  pageNumber=1;
  pageSize=12;
  isAscending=true;

  ngOnInit(): void {
   this.getproducts();
  }
 
  constructor (private shopServices:ShopService){}

  onSortSelected(event:any){
   this.sortSelected=event.target.value;
   //ürün listesi gelecek
   this.getproducts();

  }
  onCategorySelected(categoryId:string){
    this.categorySelected=categoryId;
    this.getproducts();
  }
  onGroupSelected(groupName:string)
  {
    this.groupSelected=groupName;
    this.getproducts();
  }
  getproducts(){
    this.shopServices.getProducts(this.categorySelected,this.groupSelected,this.sortSelected,this.pageNumber,this.pageSize,this.search,this.isAscending).subscribe({
      next:response=>{ this.products=response.data;
        this.totalCount=response.totalCount;
        this.pageSize=response.pageSize;
      
      },
      error:error=>console.log("hata :"+error)
    })
  }
  onSearch() {
    this.search=this.searchTerm?.nativeElement.value;
    this.getproducts();
  }

  onReset() {
    if(this.searchTerm) this.searchTerm.nativeElement.value='';
    this.sortSelected='name';
    this.sortOptions= [
      {name:'Alphabetical',value:'name'},
      {name:'Price:Low to high',value:'priceAsc'},
      {name:'Price:High to low',value:'priceDesc'}
    ]
    this.categorySelected="ALL";
    this.groupSelected="ALL";
   this. groupOptions=[
   
      {name:'All',value:'All'},
      {name:'Accessories',value:'Accessories'},
      {name:'Apparel',value:'Apparel'},
      {name:'Bags',value:'Bags'},
      {name:'Shoes',value:'Shoes'}
    ]

   this. categoryOptions=[
      {name:'All',value:'All'},
      {name:'Women',value:'Women'},
      {name:'Men',value:'Men'},
      {name:'Kids',value:'Kids'},
      {name:'Unisex',value:'Unisex'},
    ]
  }

  onPageChanges(event: any) {
    if (this.pageNumber != event) {
     this.pageNumber=event;
     this.getproducts();
    }
}

}

