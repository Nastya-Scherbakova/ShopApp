import { Component, Inject, forwardRef } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { debug } from 'util';


@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;

  constructor(private api:ApiService) {
    
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  parseHtml() {
    this.api.parse();
  }

  
}
