import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { throwError } from 'rxjs';
import Item from '../../models/item.model';
import { ActivatedRoute } from '@angular/router';
import { Chart } from 'angular-highcharts';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {
  public item: Item;
  id: number;
  chart: Chart;
  showChart:false;
  constructor(private route: ActivatedRoute, private api: ApiService) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.id = +params['id'];
      this.getItem();
    });
  }


  add() {
    this.chart.addPoint(Math.floor(Math.random() * 10));
  }

  getItem() {
    this.api.getItem(this.id).subscribe(
      item => {
        this.item = item as Item;
        this.chart = new Chart({
          chart: {
            type: 'line'
          },
          title: {
            text: 'Price changing'
          },
          xAxis: {
            type: 'datetime',
          },
          credits: {
            enabled: false
          },
          series: [
            {
              name: this.item.title,
              
            }
          ]
        });
        this.item.priceHistory.forEach(el => {
         let date = new Date(el.date);
          this.chart.addPoint([Date.UTC(date.getUTCFullYear(), date.getUTCMonth(), date.getUTCDate(), date.getHours(), date.getUTCMinutes()), el.price]);
        })
      },
      error => { throwError(error) }
    );
  }


}
