import { Component, OnInit, QueryList, ViewChildren, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { FormControl } from '@angular/forms'
import { Observable } from 'rxjs';
import { HttpServiceService } from '../../services/http-service.service';
import { Subscription } from 'rxjs';
import { manufacturer } from '../../shared/request-response';
import { Router, ActivatedRoute } from '@angular/router';
import { environment } from '../../../environments/environment';
import { shared_data } from '../../shared/shared-data';

@Component({
  selector: 'app-manufacturer-list',
  templateUrl: './manufacturer-list.component.html',
  styleUrls: ['./manufacturer-list.component.css']
})
export class ManufacturerListComponent implements OnInit {
  manufacturers$: manufacturer[];
  list = new Subscription();
  base_url = environment.base_url;
  url: string;
  constructor(private http: HttpServiceService,
    private router: Router,
    private shared: shared_data,
    private route: ActivatedRoute,) {
      this.getManufacturers();
     }

  ngOnInit(): void {
  }
  getManufacturers() {
    this.base_url=this.base_url+'Manufacturer/GetAllManufacturer'
    this.list.add(this.http._httpget(this.base_url, '').
      subscribe((result: any) => {

        if (result.status == 200) {

          this.manufacturers$ = result.body;

        }

      }, (error: any) => {
        console.log(error)

      }))
  }

}
