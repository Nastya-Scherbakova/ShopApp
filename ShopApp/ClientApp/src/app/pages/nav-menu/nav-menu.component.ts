import { Component, Inject, forwardRef } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { HomeComponent } from '../home/home.component';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;

  constructor() {
    
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  parseHtml() {
   // this.homeComponent.parseData();
  }

  
}
