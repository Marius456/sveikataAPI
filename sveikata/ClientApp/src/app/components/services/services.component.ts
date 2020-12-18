import { Component, OnInit } from '@angular/core';
import { Service } from '../../entities/service';  
import { ServiceService } from '../../services/service.service';   
import { NgForm, FormBuilder, FormGroup, Validators, FormControl, ReactiveFormsModule } from '@angular/forms';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-services',
  templateUrl: './services.component.html'
})
export class ServicesComponent implements OnInit {

  dataSaved = false;    
  message:string;    
  FromService: any;    
  Id:number=0;    
  allServices:Observable<Service[]>;    
  constructor(private formbuilder: FormBuilder,private ServiceService:ServiceService) { }    
  
  ngOnInit(): void {  
    this.FromService = this.formbuilder.group({  
      Id: [0, [Validators.required]],  
      Name: ['', [Validators.required]],  
      Description: ['', [Validators.required]],  
    });  
      this.GetService();  
  }

  UpdateId() : void{
    this.FromService.patchValue({
      Id: this.Id,
    })
  }

  Reset()    
  {    
    
    this.Id = 0;  
    this.FromService.controls['Name'].setValue('');  
    this.FromService.controls['Description'].setValue('');  
   // this.FromService.reset();  
  }

  GetService()    
  {      
    this.allServices=this.ServiceService.getServices();    
  } 

  CreateService(Service: Service) {
    if(this.Id != 0){
      this.UpdateService(this.Id, Service);
    }
    this.ServiceService.addService(Service)
    .subscribe(() => {  
            this.dataSaved = true;  
            this.message = 'Record saved Successfully';  
            this.Id = 0;
            this.Reset();  
            this.GetService();  
        });  
  }

  EditService(ServiceId: string) {  
    this.ServiceService.getService(ServiceId).subscribe(Response => {  
        this.message = null;  
        this.dataSaved = false; 
        this.Id = Response.id;  
        this.FromService.controls['Name'].setValue(Response.name);  
        this.FromService.controls['Description'].setValue(Response.description);  
    });  
  } 

  UpdateService(ServiceId: number, Service: Service) {
    this.ServiceService.updateService(ServiceId, Service)
    .subscribe(() => {  
            this.dataSaved = true;  
            this.message = 'Record saved Successfully';  
            this.Id = 0;
            this.Reset();  
            this.GetService();  
        });  
  }

  DeleteService(ServiceId: string) {  
    if (confirm("Are You Sure To Delete this Informations")) {  
        this.ServiceService.deleteService(ServiceId).subscribe(  
            () => {  
                this.dataSaved = true;  
                this.message = "Deleted Successfully";  
                this.GetService();  
            });  
    }     
  }   

  
}
