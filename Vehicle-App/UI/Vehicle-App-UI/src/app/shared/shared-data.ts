import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
  })
export class shared_data{

    categoryInfo={
        id:0,
        name:'',
        min_value:0.00,
        max_value:0.00
    }
}