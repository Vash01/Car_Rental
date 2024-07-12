import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from 'src/app/models/User';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class CarService {

  constructor(private http: HttpClient) {}

  getCars(): Observable<any> {
    const url = `${environment.Url}/api/Cars`;
    return this.http.get(url,{ headers: this.getHeaders() });
  }

  searchCars(query: any): Observable<any> {
    const url = `${environment.Url}/api/Cars/search`;
    const params = new HttpParams({ fromObject: query });
    return this.http.get(url, { headers: this.getHeaders(), params });
  }

  key:string | null = null
  private getHeaders(): HttpHeaders {
    const token = localStorage.getItem(environment.userKey); // Retrieve your JWT token from storage
    if(token){
      const user : User = JSON.parse(token);
      this.key =  user.jwt;
    }
    let headers = new HttpHeaders();
    if (this.key) {
      headers = headers.set('Authorization', `Bearer ${this.key}`);
    }

    return headers;
  }

  deleteCarRecord(query:any): Observable<any> {
    const url = `${environment.Url}/api/Cars/delete-car/${query}`;
    // const params = new HttpParams({ fromObject: query});
    return this.http.delete(url, { headers: this.getHeaders()});
  }

  addCarRecord(carDetails: any, adminEmail:any):Observable<any>{
    console.log(carDetails);
    const url = `${environment.Url}/api/Cars/AddCar/${adminEmail}`;
    const params = new HttpParams({ fromObject: carDetails});
    return this.http.post(url,carDetails, { headers: this.getHeaders()});
  }
}
