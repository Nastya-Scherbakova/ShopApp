import { Component, Input } from '@angular/core';
import Item from '../../models/item.model';

@Component({
  selector: 'item',
  templateUrl: './item.component.html',
})
export class ItemComponent {
  @Input("item")
  item: Item;
}
