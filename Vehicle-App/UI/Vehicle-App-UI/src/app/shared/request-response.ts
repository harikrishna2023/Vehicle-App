import { DecimalPipe } from "@angular/common"

export interface SuccessData {
    statusCode: number,
    statusMessage: string,
    referenceId: string,
    errorMessage: string,
    response: string
}

export interface apiResponse {
    body: any,
    headers: any,
    status: number,
    statusText: string,
    type: number,
    url: string
}

export interface apiResponseError {
    error: erroeResponseObject,
    headers: any,
    message: string,
    name: string,
    status: number,
    statusText: string,
    ok: boolean,
    url: string,
    errorMessage?: string
}

export interface erroeResponseObject {
    errorMessage: string
    referenceId: string
}

export interface request {

    create_category: {
        id:number,
        name: string,
        icon: string,
        min_value: number,
        max_value: number
    }
    create_vehicle:{
      
        owner_name:string,
        year:number,
        weight:DecimalPipe,
        manufacturer_id:number
    }
}
export interface category {
    id: number,
    name: string,
    icon: string,
    min_value: number
    max_value: number
}
export interface vehicle{
    id:number,
    owner_name:string,
    year:number,
    manufacturer_name:string,
    weight:string
    manufacturer_id:number,
    icon:string
}
export interface manufacturer{
    id:number,
    manufacturer_name:string
}
export interface categorylist{
    id: number,
    name: string,
    icon: string,
    min_value: number
    max_value: number,
    temp_id:number,
    category_id:number
}

