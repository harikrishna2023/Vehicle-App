import { Component, OnInit, QueryList, ViewChildren, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { FormControl } from '@angular/forms'
import { Observable } from 'rxjs';
import { HttpServiceService } from '../../services/http-service.service';
import { Subscription } from 'rxjs';
import { vehicle } from '../../shared/request-response';
import { Router, ActivatedRoute } from '@angular/router';
import { environment } from '../../../environments/environment';
import { shared_data } from '../../shared/shared-data';


@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.css']
})
export class VehicleListComponent implements OnInit {
  vehicles$:vehicle[];
  list=new Subscription();
  url: string;
  errorMessage='';
  constructor(private http: HttpServiceService,
    private router: Router,
    private shared: shared_data,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.getVehicles();
  }
  getVehicles(){
    this.url=environment.base_url+'vehicle/GetAllVehicle'
    this.list.add(this.http._httpget(this.url, '').
    subscribe((result: any) => {

      if (result.status == 200 ||result.status==204) {

        this.vehicles$ = result.body;
        console.log(this.vehicles$);
      }

    }, (error: any) => {
      this.errorMessage=error.error.statusMessage;

    }))
  }

  btnClick(){
    this.router.navigate(['create-vehicle'])
  }
}
