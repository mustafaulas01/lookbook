import * as cuid from "cuid";

export interface Basket {
    id: string;
    items: BasketItem[];
  }
  
  export interface BasketItem {
    productCode: string;
    productName: string;
    quantity: number;
    picturePath: string;
    category: string;
    subCategory: string;
  }

  export class Basket implements Basket {
    id=cuid();
    items: BasketItem[]=[];
  }