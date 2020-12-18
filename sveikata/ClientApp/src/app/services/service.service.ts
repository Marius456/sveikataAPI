import { Injectable } from '@angular/core';     
import { Observable } from 'rxjs';    

import {HttpClient} from '@angular/common/http';    
import {HttpHeaders} from '@angular/common/http'; 
import { Service, NewService } from '../entities/service';

@Injectable({
  providedIn: 'root'
})
export class ServiceService {
  Url = 'https://localhost:5001/api/services/';

  constructor(private http:HttpClient) { }  

  getServices():Observable<Service[]>    
  {    
    return this.http.get<Service[]>(this.Url);    
  } 

  addService(Service: NewService): Observable<Service> {
    return this.http.post<Service>(this.Url, Service)
  }  

  updateService(ServiceId: number, Service: Service): Observable<Service> {
    return this.http.put<Service>(this.Url + ServiceId, Service)
  }  

  deleteService(ServiceId:string):Observable<number>    
  {    
    return this.http.delete<number>(this.Url+ ServiceId);    
  }
  
  getService(ServiceId: string): Observable<Service> {    
    return this.http.get<Service>(this.Url + ServiceId);    
  }   
}
