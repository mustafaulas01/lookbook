import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShbodyComponent } from './shbody/shbody.component';
import { ProductItemComponent } from './product-item/product-item.component';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [
    ShbodyComponent,
    ProductItemComponent
  ],
  imports: [
    CommonModule,
    SharedModule
  ],
  exports:[ShbodyComponent,ProductItemComponent]
})
export class ShopModule { }
