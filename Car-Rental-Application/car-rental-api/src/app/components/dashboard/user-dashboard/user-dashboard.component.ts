import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { Car } from 'src/app/models/Car';
import { AccountService } from '../../account/account.service';
import { CarListComponent } from '../../car-list/car-list.component';

@Component({
  selector: 'app-user-dashboard',
  templateUrl: './user-dashboard.component.html',
  styleUrls: ['./user-dashboard.component.css']
})
export class UserDashboardComponent {

  
  constructor( private router: Router, private formBuilder: FormBuilder, public accountService: AccountService){}

  ngOnInit(): void {
    this.refreshUser();
  }

  private refreshUser() {
    const jwt = this.accountService.getJWt();
    if (jwt) {
      this.accountService.refreshUser(jwt).subscribe({
        next: (user) => {
          console.log('Token refresh successful. New user data:', user);
        },
        error: (error) => {
          console.error('Error during token refresh:', error);
          this.accountService.logout();
        },
      });
    } else {
      this.accountService.refreshUser(null).subscribe();
    }
  }
  logout(){
    this.accountService.logout();
  }

  rentalAgreement(userEmail: string): void {
    this.router.navigate(['/agreement', userEmail]);
  }
}
