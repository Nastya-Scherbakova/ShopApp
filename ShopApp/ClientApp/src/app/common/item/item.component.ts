import { Component, Input } from '@angular/core';
import Item from '../../models/item.model';

@Component({
  selector: 'item',
  templateUrl: './item.component.html',
   styleUrls: ['./item.component.css']
})
export class ItemComponent {
  @Input("item")
  item: Item;
}
