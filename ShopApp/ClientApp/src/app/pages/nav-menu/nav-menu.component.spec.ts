import { TestBed, async } from "@angular/core/testing";
import { NavMenuComponent } from "./nav-menu.component";
import { RouterTestingModule } from "@angular/router/testing";
import { MatProgressSpinnerModule } from "@angular/material/progress-spinner";
import { HttpClientModule } from "@angular/common/http";
import { Observable, BehaviorSubject } from "rxjs";


class MockApiService {
  itemsSource = new BehaviorSubject([]);
  items = this.itemsSource.asObservable();
  http:any;
  constructor(http: any) {
    this.http = http;
  }

  parse() {

  }

  getItems() {

  }

  changeItems(items) {

  }

  getItem(id: number) {
    return new Observable<any>();
  }

}

describe('NavMenuComponent', () => {
  let component: NavMenuComponent;
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
      ]
    }).compileComponents();
    service = new MockApiService(2);
     component = new NavMenuComponent(service);



  }));
  it('should load with params that was given as default', async(() => {
    expect(component.isExpanded).toBeFalsy();
    expect(component.loading).toBeFalsy();

  }));
  it('should set loading when clicking on parse', async(() => {
    component.parseHtml();
    expect(component.loading).toBeTruthy();
  }));
  it('should set isExpanding false when calling collapse', async(() => {
    component.collapse();
    expect(component.isExpanded).toBeFalsy();
  }));
   it('should toggle isExpanding when calling toggle', async(() => {
    component.toggle();
    expect(component.isExpanded).toBeTruthy();
    component.toggle();
    expect(component.isExpanded).toBeFalsy();
  }))
});
