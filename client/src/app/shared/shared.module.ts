import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginHeaderComponent } from './pagin-header/pagin-header.component';
import { PagerComponent } from './pager/pager.component';
import {PaginationModule} from 'ngx-bootstrap/pagination';


@NgModule({
  declarations: [
    PaginHeaderComponent,
    PagerComponent
  ],
  imports: [
    CommonModule,
    PaginationModule.forRoot()
    
  ],
  exports:[
    PaginHeaderComponent,
    PagerComponent
  ]
})
export class SharedModule { }
