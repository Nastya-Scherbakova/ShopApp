import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, of } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { debug } from 'util';


@Injectable()
export class ApiService {
  private itemsSource = new BehaviorSubject([])
  items = this.itemsSource.asObservable();
  constructor(private http: HttpClient) { }

  parse() {
    this.http.get('Items/Parse').subscribe(data => {
      this.changeItems(data);
      },
      err => {
       // this.itemsSource.error('Parser running in first time');
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
    return this.http.get('Items/Details/'+id)
  }

}
