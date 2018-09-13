import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { throwError } from 'rxjs';
import Item from '../../models/item.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit{
  public items: Array<Item>;

  constructor(private api: ApiService) {

  }

  ngOnInit() {
    this.getItemList();
  }

  getItemList() {
    this.api.getItems().subscribe(
      data => {
         this.items = data as Array<Item>;
         return true;
       },
       error => {
         console.error("Error getting items");
         return throwError(error); 
       }
    )
  }

  parseData() {
    this.api.parse().subscribe(
      data => {
         this.items = data as Array<Item>;
         return true;
       },
       error => {
         console.error("Error parse items");
         return throwError(error); 
       }
    )
  }
}
