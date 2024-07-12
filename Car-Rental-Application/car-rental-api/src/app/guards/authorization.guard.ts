import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { map, Observable, take } from 'rxjs';
import { AccountService } from '../components/account/account.service';
import { User } from '../models/User';

@Injectable({
  providedIn: 'root'
})
export class AuthorizationGuard{

  constructor(private accountService: AccountService, private router: Router ){}
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> {
    return this.accountService.user$.pipe(
      take(1),
      map((user: User | null)=>{
        if(user){
          console.log("auth guard is working fine");
          return true;
        }
        else{
          console.log("Restricted area, leave immediately");
          this.router.navigate(['./account/login'],{queryParams: {returnUrl: state.url}});
          return false;
        }
      })
    );
  }
  
}
