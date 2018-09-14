import { Component, OnInit, AfterViewInit, AfterContentInit } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { throwError, Subscription } from 'rxjs';
import Item from '../../models/item.model';
import { debug } from 'util';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit, AfterContentInit {
  public items: Array<Item>;
  public loading = true;
  public onlyInit: boolean = true;

  constructor(private api: ApiService, public snackBar: MatSnackBar) {

  }

  ngOnInit() {
    this.getItemList();
    this.loading = true;
    this.api.items.subscribe(items => {
      this.items = items as Array<Item>;

      if (!this.onlyInit) {
        setTimeout(() => {
          this.openSnackBar("Done", "Ok");
          this.loading = false;
        }, 500);

      }



    },
      err => {
        this.openSnackBar(err, "Ok", 3000);
      }

    );
    this.loading = true;
  }
  ngAfterContentInit() {
    this.onlyInit = false;
  }

  openSnackBar(message: string, action: string, time?: number) {
    if (!time) {
      time = 2000;
    }
    this.snackBar.open(message, action, {
      duration: time,
    });
  }

  getItemList() {
    this.api.getItems();
  }

  parseData() {
    return this.api.parse();
  }
}
