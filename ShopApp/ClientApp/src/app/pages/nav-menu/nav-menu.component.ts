import { Component, Inject, forwardRef, OnInit } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { debug } from 'util';
import { MatSnackBar } from '@angular/material/snack-bar';


@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit{
  isExpanded = false;
  public loading: boolean = false;

  constructor(private api:ApiService, public snackBar: MatSnackBar) {
    
  }

  ngOnInit() {
    this.api.items.subscribe(items => {
          this.loading = false;
    }
    );
  }


  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  parseHtml() {
    this.loading = true;
    this.api.parse();
  }

  
}
