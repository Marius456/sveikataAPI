import { Injectable } from '@angular/core';     
import { Observable } from 'rxjs';    

import {HttpClient} from '@angular/common/http';    
import {HttpHeaders} from '@angular/common/http'; 
import { User, NewUser } from '../entities/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  Url = 'https://localhost:5001/api/users/';

  constructor(private http:HttpClient) { }  

  getUsers():Observable<User[]>    
  {    
    return this.http.get<User[]>(this.Url);    
  } 

  addUser(User: NewUser): Observable<User> {
    return this.http.post<User>(this.Url, User)
  }  

  updateUser(UserId: number, User: User): Observable<User> {
    return this.http.put<User>(this.Url + UserId, User)
  }  

  deleteUser(UserId:string):Observable<number>    
  {    
    return this.http.delete<number>(this.Url+ UserId);    
  }
  
  getUser(UserId: string): Observable<User> {    
    return this.http.get<User>(this.Url + UserId);    
  }   
}
