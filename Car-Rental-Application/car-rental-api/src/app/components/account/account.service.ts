import { Observable, catchError, ReplaySubject, map, of } from 'rxjs';
import { ErrorHandler, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import {Register} from '../../models/register'
import { environment } from 'src/environments/environment.development';
import { Login } from 'src/app/models/Login';
import { User } from 'src/app/models/User';
@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private userSource = new ReplaySubject<User | null>(1);
  user$ = this.userSource.asObservable();

  constructor(private router: Router, private http: HttpClient) { 
  }


  refreshUser(jwt: string |null){
    if(jwt==null){
    this.userSource.next(null);
    return of(undefined);
    }

    let headers = new HttpHeaders();
    headers = headers.set('Authorization', 'Bearer ' + jwt);

    return this.http.get<User>(`${environment.Url}/api/account/refresh-user-token`,{headers}).pipe
    (map((user:User)=>{
      if(user){
        //console.log("setting local storage");
        this.setUser(user);
        
      }
      
    }));
  }

  register(model: Register){
    
    return this.http.post(`${environment.Url}/api/account/register`,model)
    
  }

  login(model:Login){
    //console.log("user details");
    return this.http.post<User>(`${environment.Url}/api/account/login`,model)
    .pipe
    (map((user:User)=>{
      if(user){
        //console.log("setting local storage");
        this.setUser(user);
        return user;
      }
      return null;
    }));
  }

  logout(){
    localStorage.removeItem(environment.userKey);
    this.userSource.next(null);
    this.router.navigateByUrl("/account/login");
  }

  getJWt(){
    const key = localStorage.getItem(environment.userKey);
    if(key){
      const user : User = JSON.parse(key);
      return user.jwt;
    }
    else{
      return null;
    }
  }

  private setUser(user: User){
    localStorage.setItem(environment.userKey,JSON.stringify(user));
    //console.log("local storage:");
    //console.log(user);
    this.userSource.next(user);
  }
}