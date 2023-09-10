import { NgModule,CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CreateVehicleComponent } from './vehicle/create-vehicle/create-vehicle.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {HttpServiceService} from './services/http-service.service';
import { HttpClientModule } from '@angular/common/http';
import{TwoDigitDecimaNumberDirective} from './shared/directives/validate-decimal'
import { RouterModule } from '@angular/router';
import { VehicleListComponent } from './vehicle/vehicle-list/vehicle-list.component';
import{DropdownDirective} from './shared/directives/dropdown-directive';

import { ManufacturerListComponent } from './manufacturer/manufacturer-list/manufacturer-list.component';
import { ManageCategoryComponent } from './category/manage-category/manage-category.component';

@NgModule({
  declarations: [
    AppComponent,
    CreateVehicleComponent,
    TwoDigitDecimaNumberDirective,
    VehicleListComponent,
    DropdownDirective,
    ManufacturerListComponent,
        ManageCategoryComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    HttpClientModule,
    RouterModule,
    
  ],
  schemas:[CUSTOM_ELEMENTS_SCHEMA],
  providers: [HttpServiceService],
  bootstrap: [AppComponent]
})
export class AppModule { }
