import { Component, OnInit } from '@angular/core';
import { Comment } from '../../entities/comment';  
import { CommentService } from '../../services/comment.service';   
import { NgForm, FormBuilder, FormGroup, Validators, FormControl, ReactiveFormsModule } from '@angular/forms';
import { Observable } from 'rxjs';
import { UserService } from '@/services/user.service';
import { User } from '@/entities/user';

@Component({
  selector: 'app-comments',
  templateUrl: './comments.component.html'
})
export class CommentsComponent implements OnInit {

  allUsers:Observable<User[]>; 
  foundUser:Observable<User>;
  dataSaved = false;    
  message:string;    
  FromComment: any;    
  Id:number=0;    
  allComments:Observable<Comment[]>;    
  constructor(private formbuilder: FormBuilder,
    private CommentService:CommentService,
    private UserService:UserService) { }    
  
  ngOnInit(): void {  
    this.FromComment = this.formbuilder.group({  
      Id: [0, [Validators.required]],  
      UserId: [null, [Validators.required]],  
      Text: ['', [Validators.required]],  
    });  
      this.GetComment();  
  }

  UpdateId() : void{
    this.FromComment.patchValue({
      Id: this.Id,
    })
  }

  Reset()    
  {    
    
    this.Id = 0;  
    this.FromComment.controls['UserId'].setValue(null);  
    this.FromComment.controls['Text'].setValue('');  
   // this.FromComment.reset();  
  }

  GetComment()    
  {      
    this.allComments=this.CommentService.getComments();    
    this.allUsers= this.UserService.getUsers(); 
  } 
  
  GetUser(id : string)    
  {         
    this.foundUser = this.UserService.getUser(id); 
  } 

  CreateComment(Comment: Comment) {
    if(this.Id != 0){
      this.UpdateComment(this.Id, Comment);
    }
    this.CommentService.addComment(Comment)
    .subscribe(() => {  
            this.dataSaved = true;  
            this.message = 'Record saved Successfully';  
            this.Id = 0;
            this.Reset();  
            this.GetComment();  
        });  
  }

  EditComment(CommentId: string) {  
    this.CommentService.getComment(CommentId).subscribe(Response => {  
        this.message = null;  
        this.dataSaved = false; 
        this.Id = Response.id;  
        this.FromComment.controls['UserId'].setValue(Response.userId);  
        this.FromComment.controls['Text'].setValue(Response.text);  
    });  
  } 

  UpdateComment(CommentId: number, Comment: Comment) {
    this.CommentService.updateComment(CommentId, Comment)
    .subscribe(() => {  
            this.dataSaved = true;  
            this.message = 'Record saved Successfully';  
            this.Id = 0;
            this.Reset();  
            this.GetComment();  
        });  
  }

  DeleteComment(CommentId: string) {  
    if (confirm("Are You Sure To Delete this Informations")) {  
        this.CommentService.deleteComment(CommentId).subscribe(  
            () => {  
                this.dataSaved = true;  
                this.message = "Deleted Successfully";  
                this.GetComment();  
            });  
    }     
  }  
}
