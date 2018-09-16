import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, of } from 'rxjs';



@Injectable()
export class ApiService {
  itemsSource = new BehaviorSubject([]);
  items = this.itemsSource.asObservable();
  http: HttpClient;
  constructor(http: HttpClient) {
    this.http = http;
  }

  parse() {
    this.http.get('Items/Parse').subscribe(data => {
      this.changeItems(data);
      },
      err => {

        this.getItems();
        });
  }

  getItems() {
    this.http.get('Items').subscribe(data => {

      this.changeItems(data);
      });
  }

  changeItems(items) {
    this.itemsSource.next(items);
  }

  getItem(id: number) {
    return this.http.get('Items/Details/'+id);
  }

}
