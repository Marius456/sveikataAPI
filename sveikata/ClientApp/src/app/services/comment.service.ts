import { Injectable } from '@angular/core';     
import { Observable } from 'rxjs';    

import {HttpClient} from '@angular/common/http';    
import {HttpHeaders} from '@angular/common/http'; 
import { Comment, NewComment } from '../entities/comment';

@Injectable({
  providedIn: 'root'
})
export class CommentService {
  Url = 'https://localhost:5001/api/comments/';

  constructor(private http:HttpClient) { }  

  getComments():Observable<Comment[]>    
  {    
    return this.http.get<Comment[]>(this.Url);    
  } 

  addComment(comment: NewComment): Observable<Comment> {
    return this.http.post<Comment>(this.Url, comment)
  }  

  updateComment(CommentId: number, comment: Comment): Observable<Comment> {
    return this.http.put<Comment>(this.Url + CommentId, comment)
  }  

  deleteComment(CommentId:string):Observable<number>    
  {    
    return this.http.delete<number>(this.Url+ CommentId);    
  }
  
  getComment(CommentId: string): Observable<Comment> {    
    return this.http.get<Comment>(this.Url + CommentId);    
  }   
}
