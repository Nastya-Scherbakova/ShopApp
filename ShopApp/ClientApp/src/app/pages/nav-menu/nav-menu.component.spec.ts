import { TestBed, async, ComponentFixture } from "@angular/core/testing";

import { ApiService } from "../../services/api.service";
import { NavMenuComponent } from "./nav-menu.component";
import { DebugElement } from "@angular/core";
import { By } from "@angular/platform-browser";
import { MatCardModule } from "@angular/material/card";
import { MatIconModule } from "@angular/material/icon";
import {RouterTestingModule} from "@angular/router/testing";
import { MatProgressSpinnerModule } from "@angular/material/progress-spinner";
import { HttpClientModule } from "@angular/common/http";
import { Observable, BehaviorSubject } from "rxjs";
import { HttpClient } from "selenium-webdriver/http";

class MockApiService {
  private itemsSource :any;
  items: any;
  http: HttpClient;
  constructor() { }

  parse() {
  
  }

  getItems() {
 
  }

  changeItems(items) {
   
  }

  getItem(id: number) {
   
  }

}

describe('NavMenuComponent', () => {
  let component: NavMenuComponent;
  let fixture: ComponentFixture<NavMenuComponent>;
  let parseHtml: DebugElement;
  let price: DebugElement;
  let service: MockApiService;
  beforeEach(async(() => {
  
    TestBed.configureTestingModule({
      imports: [
        MatProgressSpinnerModule,
        RouterTestingModule,
        HttpClientModule
      ],
      declarations: [
        NavMenuComponent
      ],
      providers: [ApiService]
    }).compileComponents();
   service = new MockApiService();
  //  component = new NavMenuComponent(service);
    parseHtml = fixture.debugElement.query(By.css('li'));
    //price = fixture.debugElement.query(By.css('mat-card-subtitle'));

    
  }));
  it('should load with params that was given as default', async(() => {
    expect(component.isExpanded).toBeFalsy();
    expect(component.loading).toBeFalsy();
   
  }));
  //it('should set loading when clicking on parse', async(() => {
  //  component.parseHtml.
  //  parseHtml.triggerEventHandler('click', null);
  //  expect(component.loading).toBeTruthy();
  //}));
  //it('should load the given price', async(() => {
  //  component.item = item;
  //  fixture.detectChanges();
  //  expect(price.nativeElement.innerHTML).toBe("₴"+item.currentPrice.toString()+".00");
  //}))
});
