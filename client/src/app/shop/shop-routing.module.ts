import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShbodyComponent } from './shbody/shbody.component';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes= [
  {path:'',component:ShbodyComponent},
  {path:':id',component:ProductDetailsComponent},
]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports:[
    RouterModule
  ]
})
export class ShopRoutingModule { }
