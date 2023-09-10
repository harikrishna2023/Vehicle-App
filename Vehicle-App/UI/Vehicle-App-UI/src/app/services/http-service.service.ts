import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable, Observer, throwError} from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import {apiResponse,apiResponseError} from '../shared/request-response'


@Injectable({
  providedIn: 'root'
})
export class HttpServiceService {
  base_url = 'https://localhost:7137/api/';
  constructor(private httpClient: HttpClient,) { }



getHttpOption() {
  return {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      
    })
  }
}


getHttpFormOption() {
  return {
    headers: new HttpHeaders({
     // 'Content-Type': 'multipart/form-data',
      
    })
  }
}

public _httppost(api_url: string, requestParams: any): Observable<any> {

  return this.httpClient
    .post(api_url,
      JSON.stringify(requestParams), {
      headers: this.getHttpOption().headers,
      observe: 'response',
    })
    .pipe(map(
      (response:any) => {
        
        return response;
      }
    ),
      catchError((err: HttpErrorResponse) => {
       
        return throwError(err);
      }));
}

public _httpget(api_url: string, options?: any, requestParams?: any): Observable<any> {
  return this.httpClient
    .get(api_url,
      {
        headers: this.getHttpOption().headers,
        observe: 'response',
        params: requestParams
      })
    .pipe(map((response: any) => {
      
      return response;
    }
    ),
      catchError((error: HttpErrorResponse) => {
       
        return throwError(error);
      }));
}

public _post(api_url: string, requestParams: FormData): Observable<any> {

  return this.httpClient
    .post(api_url,
      requestParams, {
      headers: this.getHttpFormOption().headers,
      observe: 'response',
    })
    .pipe(map(
      (response:any) => {
        
        return response;
      }
    ),
      catchError((err: HttpErrorResponse) => {
       
        return throwError(err);
      }));
}

}