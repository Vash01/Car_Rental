import { Observable, catchError, ReplaySubject, map, of } from 'rxjs';
import { ErrorHandler, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import {Register} from '../../models/register'
import { environment } from 'src/environments/environment.development';
import { Login } from 'src/app/models/Login';
import { User } from 'src/app/models/User';
import { AccountService } from '../account/account.service';
import { Rental } from 'src/app/models/Rental';
@Injectable({
  providedIn: 'root'
})
export class UserService {
  
  
    constructor(private router: Router, private http: HttpClient, private accountService: AccountService) {}
    
    
    getUserByEmail(userEmail: string): Observable<any> {
      const  token = this.accountService.getJWt();

    let headers = new HttpHeaders();
    headers = headers.set('Authorization', 'Bearer ' + token);

        const url = `${environment.Url}/api/User/${userEmail}`;
         return this.http.get(url,{headers});
    }

    getUserRentalAgreements(userEmail: string): Observable<any> {

      const  token = this.accountService.getJWt();

    let headers = new HttpHeaders();
    headers = headers.set('Authorization', 'Bearer ' + token);

      const url = `${environment.Url}/api/User/${userEmail}/rental-agreements`;
      return this.http.get(url, { headers });
    }

    acceptRentalAgreement(userEmail: string, carId: Number, duration:any): Observable<boolean> {
      const  token = this.accountService.getJWt();
      let headers = new HttpHeaders();
      headers = headers.set('Authorization', 'Bearer ' + token);
      const url = `${environment.Url}/api/User/${userEmail}/accept-rental-agreement/${carId}?duration=${duration}`;
      
       return this.http.put<boolean>(url, {headers});
    }

    editRentalAgreement(userEmail: string, carId: Number, Duration:Number): Observable<boolean> {
      const  token = this.accountService.getJWt();

      let headers = new HttpHeaders();
      headers = headers.set('Authorization', 'Bearer ' + token);
      const url = `${environment.Url}/api/User/${userEmail}/edit-rental-agreement/${carId}`;
      
      return this.http.put<boolean>(url, Duration, {headers});
    }

    returnRequest(userEmail: string, carId: Number): Observable<boolean> {
      const  token = this.accountService.getJWt();
      let headers = new HttpHeaders();
      headers = headers.set('Authorization', 'Bearer ' + token);
      const url = `${environment.Url}/api/User/${userEmail}/request-return/${carId}`;
      
       return this.http.post<boolean>(url, {headers});
    }
     
    // services for admin
    // Get all rental agreements
    getAllRentalAgreements(): Observable<any> {
      const  token = this.accountService.getJWt();
      let headers = new HttpHeaders();
      headers = headers.set('Authorization', 'Bearer ' + token);
    const url = `${environment.Url}/api/Admin/rental-agreements`;
    return this.http.get(url,{headers});
    }

    //Get all return requests
    getReturnRequestCars(): Observable<any> {
      const  token = this.accountService.getJWt();
      let headers = new HttpHeaders();
      headers = headers.set('Authorization', 'Bearer ' + token);
    const url = `${environment.Url}/api/Admin/return-request`;
    return this.http.get(url,{headers});
    }

    //Accept return requests
    acceptReturnRequests(carId:Number, userId:string,adminEmail:string, result:boolean): Observable<any> {
      const  token = this.accountService.getJWt();
      let headers = new HttpHeaders();
      headers = headers.set('Authorization', 'Bearer ' + token);
    const url = `${environment.Url}/api/Admin/accept-return-requests`;

    const requestBody = {
      CarId: carId,
      UserId: userId,
      ReturnResult: result,
      AdminEmail: adminEmail
    };

    return this.http.put(url, requestBody,{headers});
    }

    
}