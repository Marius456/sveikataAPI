import { Component, OnInit } from '@angular/core';
import { User } from '../../entities/user';  
import { UserService } from '../../services/user.service';  
import { NgForm, FormBuilder, FormGroup, Validators, FormControl, ReactiveFormsModule } from '@angular/forms';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-services',
  templateUrl: './users.component.html'
})
export class UsersComponent implements OnInit {

  dataSaved = false;    
  message:string;    
  FromUser: any;    
  Id:number=0;    
  allUsers:Observable<User[]>;    
  constructor(private formbuilder: FormBuilder,private UserService:UserService) { }    
  
  ngOnInit(): void {  
    this.FromUser = this.formbuilder.group({  
      Id: [0, [Validators.required]],  
      Name: ['', [Validators.required]],  
      Email: ['', [Validators.required]],  
      Password: ['', [Validators.required]],  
    });  
      this.GetUser();  
  }

  UpdateId() : void{
    this.FromUser.patchValue({
      Id: this.Id,
    })
  }

  Reset()    
  {    
    
    this.Id = 0;  
    this.FromUser.controls['Name'].setValue('');  
    this.FromUser.controls['Email'].setValue('');  
    this.FromUser.controls['Password'].setValue('');   
   // this.FromService.reset();  
  }

  GetUser()    
  {      
    this.allUsers=this.UserService.getUsers();    
  } 

  CreateUser(User: User) {
    if(this.Id != 0){
      this.UpdateUser(this.Id, User);
    }
    this.UserService.addUser(User)
    .subscribe(() => {  
            this.dataSaved = true;  
            this.message = 'Record saved Successfully';  
            this.Id = 0;
            this.Reset();  
            this.GetUser();  
        });  
  }

  EditUser(UserId: string) {  
    this.UserService.getUser(UserId).subscribe(Response => {  
        this.message = null;  
        this.dataSaved = false; 
        this.Id = Response.id;  
        this.FromUser.controls['Name'].setValue(Response.name);  
        this.FromUser.controls['Email'].setValue(Response.email);  
        this.FromUser.controls['Password'].setValue(Response.password);  
    });  
  } 

  UpdateUser(UserId: number, User: User) {
    this.UserService.updateUser(UserId, User)
    .subscribe(() => {  
            this.dataSaved = true;  
            this.message = 'Record saved Successfully';  
            this.Id = 0;
            this.Reset();  
            this.GetUser();  
        });  
  }

  DeleteUser(UserId: string) {  
    if (confirm("Are You Sure To Delete this Informations")) {  
        this.UserService.deleteUser(UserId).subscribe(  
            () => {  
                this.dataSaved = true;  
                this.message = "Deleted Successfully";  
                this.GetUser();  
            });  
    }     
  }   
}
