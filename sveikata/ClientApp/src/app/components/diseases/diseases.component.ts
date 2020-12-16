import { Component, OnInit } from '@angular/core';
import { Disease } from '../../entities/disease';  
import { DiseaseService } from '../../services/disease.service';   
import { NgForm, FormBuilder, FormGroup, Validators, FormControl, ReactiveFormsModule } from '@angular/forms';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-diseases',
  templateUrl: './diseases.component.html'
})
export class DiseasesComponent implements OnInit {

  dataSaved = false;    
  message:string;    
  FromDisease: any;    
  Id:number=0;    
  allDiseases:Observable<Disease[]>;    
  constructor(private formbuilder: FormBuilder,private DiseaseService:DiseaseService) { }    
  
  ngOnInit(): void {  
    this.FromDisease = this.formbuilder.group({  
      Id: [0, [Validators.required]],  
      Name: ['', [Validators.required]],  
      Description: ['', [Validators.required]],  
    });  
      this.GetDisease();  
  }

  UpdateId() : void{
    this.FromDisease.patchValue({
      Id: this.Id,
    })
    debugger;
  }

  Reset()    
  {    
    
    this.Id = 0;  
    this.FromDisease.controls['Name'].setValue('');  
    this.FromDisease.controls['Description'].setValue('');  
   // this.FromDisease.reset();  
  }

  GetDisease()    
  {      
    this.allDiseases=this.DiseaseService.getDiseases();    
  } 

  CreateDisease(Disease: Disease) {
    if(this.Id != 0){
      this.UpdateDisease(this.Id, Disease);
    }
    this.DiseaseService.addDisease(Disease)
    .subscribe(() => {  
            this.dataSaved = true;  
            this.message = 'Record saved Successfully';  
            this.Id = 0;
            this.Reset();  
            this.GetDisease();  
        });  
  }

  EditDisease(DiseaseId: string) {  
    this.DiseaseService.getDisease(DiseaseId).subscribe(Response => {  
        this.message = null;  
        this.dataSaved = false; 
        this.Id = Response.id;  
        this.FromDisease.controls['Name'].setValue(Response.name);  
        this.FromDisease.controls['Description'].setValue(Response.description);  
    });  
  } 

  UpdateDisease(DiseaseId: number, Disease: Disease) {
    
    debugger;

    this.DiseaseService.updateDisease(DiseaseId, Disease)
    .subscribe(() => {  
            this.dataSaved = true;  
            this.message = 'Record saved Successfully';  
            this.Id = 0;
            this.Reset();  
            this.GetDisease();  
        });  
  }

  DeleteDisease(DiseaseId: string) {  
    if (confirm("Are You Sure To Delete this Informations")) {  
        this.DiseaseService.deleteDisease(DiseaseId).subscribe(  
            () => {  
                this.dataSaved = true;  
                this.message = "Deleted Successfully";  
                this.GetDisease();  
            });  
    }     
  }   

  
}
