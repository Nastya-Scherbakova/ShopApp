import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class ApiService {

  constructor(private http: HttpClient) { }

  parse() {
    return this.http.get('Items/Parse');
  }

  getItems() {
    return this.http.get('Items');
  }

}
