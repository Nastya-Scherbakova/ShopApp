import PriceInfo from "./price-info.model";

export default class Item {
  id:number;
  title: string;
  about: string;
  image:string;
  currentPrice: number;
  priceHistory: Array<PriceInfo>;
}


