import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {CreateVehicleComponent} from './vehicle/create-vehicle/create-vehicle.component';
import{VehicleListComponent} from './vehicle/vehicle-list/vehicle-list.component';
import {ManufacturerListComponent} from './manufacturer/manufacturer-list/manufacturer-list.component';
import{ManageCategoryComponent} from './category/manage-category/manage-category.component';
const routes: Routes = [
  {
    path:'',
    component:ManageCategoryComponent
   
  },

  {
    path:'vehicle-list',
    component:VehicleListComponent
  },
  {
    path:'create-vehicle',
    component:CreateVehicleComponent
  },
  {
    path:'manufacturer-list',
    component:ManufacturerListComponent
  },
  {
    path:'manage-category',
    component:ManageCategoryComponent
  }
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
