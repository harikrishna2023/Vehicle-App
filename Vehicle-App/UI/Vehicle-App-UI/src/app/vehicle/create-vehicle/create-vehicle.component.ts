import { Component, OnInit, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { FormGroup, FormControl, FormArray, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { HttpServiceService } from '../../services/http-service.service';
import { Subscription } from 'rxjs';
import { manufacturer } from '../../shared/request-response'
import { environment } from 'src/environments/environment';
import { request } from '../../shared/request-response'

@Component({
  selector: 'app-create-vehicle',
  templateUrl: './create-vehicle.component.html',
  styleUrls: ['./create-vehicle.component.css']
})
export class CreateVehicleComponent implements OnInit {
  listData = new Subscription();
  save = new Subscription();
  reqVehicle: request['create_vehicle'];
  vehicle: any;
  manufacturers: manufacturer[];
  base_url: string;
  errorMessage='';
  constructor(private formBuilder: FormBuilder,
    private http: HttpServiceService,
    private router: Router) {
    this.GetManufactures();
  }

  public VehicleForm: FormGroup = this.formBuilder.group({
    name: (['', Validators.required]),
    year: (['', Validators.required]),
    weight: (['', Validators.required]),
    manufacturer_name: (['', Validators.required])
  }
  );

  ngOnInit(): void {
  }

  AddVehicle() {

    debugger;
    console.log(this.VehicleForm.value);
    this.base_url=environment.base_url+'vehicle/AddVehicle'
    this.reqVehicle = {
     
      owner_name: this.VehicleForm.value.name,
      year: this.VehicleForm.value.year,
      weight: this.VehicleForm.value.weight,
      manufacturer_id: this.VehicleForm.value.manufacturer_name
    }
    

    this.save.add(this.http._httppost(this.base_url, this.reqVehicle).
      subscribe((result: any) => {

        if(result.status==200 || result.status==204){
          this.router.navigate(['vehicle-list']);
        }

      }, (error: any) => {
        this.errorMessage=error.error.statusMessage;

      }))


  }

  numberOnly(event): boolean {
    const charCode = (event.which) ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;

  }
  
  GetManufactures() {

    this.base_url = environment.base_url + 'Manufacturer/GetAllManufacturer';
    this.listData.add(this.http._httpget(this.base_url, '').
      subscribe((result: any) => {

        if (result.status == 200) {

          this.manufacturers = result.body;
        }

      }, (error: any) => {
        this.errorMessage=error.error.statusMessage;

      }))
  }
  goToList(){
    this.router.navigate(['vehicle-list']);
  }

 
}
