import { TestBed, async, ComponentFixture } from "@angular/core/testing";

import { ApiService } from "../../services/api.service";
import { ItemComponent } from "./item.component";
import Item from "../../models/item.model";
import { DebugElement } from "@angular/core";
import { By } from "@angular/platform-browser";
import { MatCardModule } from "@angular/material/card";
import { MatIconModule } from "@angular/material/icon";
import {RouterTestingModule} from "@angular/router/testing";

describe('ItemComponent', () => {
  let component: ItemComponent;
  let fixture: ComponentFixture<ItemComponent>;
  let item: Item;
  let title: DebugElement;
  let price: DebugElement;
  beforeEach(async(() => {
    item = {
      title: "Shop item",
      about: "Item about",
      currentPrice: 788,
      priceHistory: [],
      id: 1,
      image: "image/href"
    }
    TestBed.configureTestingModule({
      imports: [
        MatCardModule,
        MatIconModule,
        RouterTestingModule
      ],
      declarations: [
        ItemComponent
      ],
      providers: [ApiService]
    }).compileComponents();
    fixture = TestBed.createComponent(ItemComponent);
    component = fixture.componentInstance;
    title = fixture.debugElement.query(By.css('mat-card-title'));
    price = fixture.debugElement.query(By.css('mat-card-subtitle'));

    
  }));
  it('should load not empty data', async(() => {
    component.item = item;
    fixture.detectChanges();
    expect(title.nativeElement.innerHTML).toBeDefined();
    expect(price.nativeElement.innerHTML).toBeDefined();
  }));
  it('should load the given title', async(() => {
    component.item = item;
    fixture.detectChanges();
    expect(title.nativeElement.innerHTML).toBe(item.title);
  }));
  it('should load the given price', async(() => {
    component.item = item;
    fixture.detectChanges();
    expect(price.nativeElement.innerHTML).toBe("₴"+item.currentPrice.toString()+".00");
  }))
});
