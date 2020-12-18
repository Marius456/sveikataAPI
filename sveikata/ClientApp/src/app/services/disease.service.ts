import { Injectable } from '@angular/core';     
import { Observable } from 'rxjs';    

import {HttpClient} from '@angular/common/http';    
import {HttpHeaders} from '@angular/common/http'; 
import { Disease, NewDisease } from '../entities/disease';

@Injectable({
  providedIn: 'root'
})
export class DiseaseService {
  Url = 'https://localhost:5001/api/diseases/';

  constructor(private http:HttpClient) { }  

  getDiseases():Observable<Disease[]>    
  {    
    return this.http.get<Disease[]>(this.Url);    
  } 

  addDisease(disease: NewDisease): Observable<Disease> {
    return this.http.post<Disease>(this.Url, disease)
  }  

  updateDisease(DiseaseId: number, disease: Disease): Observable<Disease> {
    return this.http.put<Disease>(this.Url + DiseaseId, disease)
  }  

  deleteDisease(DiseaseId:string):Observable<number>    
  {    
    return this.http.delete<number>(this.Url+ DiseaseId);    
  }
  
  getDisease(DiseaseId: string): Observable<Disease> {    
    return this.http.get<Disease>(this.Url + DiseaseId);    
  }   
}
