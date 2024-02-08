import { Component, Input,EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-pager',
  templateUrl: './pager.component.html',
  styleUrls: ['./pager.component.scss']
})
export class PagerComponent {

  @Input() pageSize?:number;
  @Input() totalCount?:number;
  @Output() pageChangeschid=new EventEmitter<number>();

  onPagerChanged(event:any) {
    this.pageChangeschid.emit(event.page);
  }

}
