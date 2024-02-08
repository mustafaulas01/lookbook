import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShbodyComponent } from './shbody/shbody.component';
import { ProductItemComponent } from './product-item/product-item.component';
import { SharedModule } from '../shared/shared.module';
import { ProductDetailsComponent } from './product-details/product-details.component';

import { ShopRoutingModule } from './shop-routing.module';



@NgModule({
  declarations: [
    ShbodyComponent,
    ProductItemComponent,
    ProductDetailsComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    ShopRoutingModule
  ],
  exports:[ProductItemComponent]
})
export class ShopModule { }
