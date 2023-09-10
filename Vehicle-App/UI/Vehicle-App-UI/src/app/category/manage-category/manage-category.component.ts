import { Component, OnInit, QueryList, ViewChildren, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms'
import { Observable } from 'rxjs';
import { HttpServiceService } from '../../services/http-service.service';
import { Subscription } from 'rxjs';
import { categorylist } from '../../shared/request-response';
import { Router, ActivatedRoute } from '@angular/router';
import { environment } from '../../../environments/environment';
import { shared_data } from '../../shared/shared-data';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-manage-category',
  templateUrl: './manage-category.component.html',
  styleUrls: ['./manage-category.component.css']
})
export class ManageCategoryComponent implements OnInit {
  categories: categorylist[];
  base_url = environment.base_url ;
  errorMessage = '';
  Category = new Subscription();
  save= new Subscription();
  del=new Subscription();
  categoryTable: FormGroup;
  control: FormArray;
  isEditable = false;
  tempData: any;
  formData: FormData = new FormData();
  constructor(private http: HttpServiceService,
    private router: Router,
    private shared: shared_data,
    private route: ActivatedRoute,
    private sanitizer: DomSanitizer,
    private fb: FormBuilder) {

  }

  ngOnInit(): void {
    // this.categoryTable = this.fb.group({
    //   tableRows: this.fb.array([])
    // });
    this.GetCategories();
  }

  public CategoryForm: FormGroup = this.fb.group({
    name: (['', Validators.required]),
    minValue: (['', Validators.required]),
    maxValue: (['', Validators.required]),
    icon: new FormControl(null),
    category_id:(['']),
    temp_id:([''])
  }
  );

  GetCategories() {
    this.base_url = environment.base_url + 'Category/ListCategory';
    this.categories=[];
    this.Category.add(this.http._httpget(this.base_url, '').
      subscribe((result: any) => {

        if (result.status == 200) {

          this.categories = result.body;

        }

      }, (error: any) => {
        this.errorMessage = error.error.statusMessage;

      }))
  }

  //adding image icon to form data
  upload(file) {
    this.formData.append("file", file[0]);

  }

  AddNew() {
    
console.log(this.CategoryForm.value);
    let url :string;
    
    if(this.CategoryForm.value.category_id!=null && this.CategoryForm.value.category_id!=undefined ){
      this.base_url = environment.base_url + 'Category/EditItem';
    }
    else{
      this.base_url = environment.base_url + 'Category/AddItem';
    }
    
    // if (this.CategoryForm.value.maxValue <= this.CategoryForm.value.minValue) {
      
    //   this.errorMessage = 'Please correct weight.'+ this.CategoryForm.value.minValue + this.CategoryForm.value.maxValue;
    //   return;
    // }
    if (this.formData.get('file') == null && this.CategoryForm.value.category_id==undefined ) {
      this.errorMessage = 'Please select category icon';
      return;
    }
    this.formData.append("id", this.CategoryForm.value.category_id);
    this.formData.append("temp_id", this.CategoryForm.value.temp_id);
    this.formData.append("name", this.CategoryForm.value.name);
    this.formData.append("icon", 'test.jpg');
    this.formData.append("min_value", this.CategoryForm.value.minValue);
    this.formData.append("max_value", this.CategoryForm.value.maxValue);

    this.Category.add(this.http._post(this.base_url, this.formData).
      subscribe((result: any) => {

        if (result.status == 200) {
          this.formData = new FormData();
          
          this.categories = this.categories.filter(item => item.temp_id !== this.CategoryForm.value.temp_id);

          this.CategoryForm.patchValue({
            name: '',
            minValue: '',
            maxValue: '',
            icon: ''
          })
          debugger;
          console.log(result.body);

          this.categories.push(result.body);

        }

      }, (error: any) => {

        this.errorMessage = error.error.statusMessage;

      }))


  }

  editData(item: any) {

    this.CategoryForm.patchValue({
      name: item.name,
      minValue: item.min_value,
      maxValue: item.max_value,
      icon: '',
      temp_id: item.temp_id,
      category_id: item.category_id
    })
    
  }
  DeleteData(item:any){

    let 
      id= item?.category_id==null && item?.category_id==undefined?item.temp_id:item.category_id
    
    this.base_url = environment.base_url + 'Category/DeleteCategory';
    this.del.add(this.http._httppost(this.base_url, id).
      subscribe((result: any) => {

        if (result.status == 200) {
          this.categories = this.categories.filter(x => x !== item);
        }

      }, (error: any) => {
        this.errorMessage=error.error.statusMessage;

      }))

     

  }
 
  saveData(){
    debugger;
    this.base_url = environment.base_url + 'Category/saveData';
    this.save.add(this.http._httppost(this.base_url, '').
      subscribe((result: any) => {

        if (result.status == 200) {
       
          this.GetCategories();
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
  


}
