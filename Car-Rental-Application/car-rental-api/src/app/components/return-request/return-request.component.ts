import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../account/account.service';
import { UserService } from '../dashboard/dashboard.service';

@Component({
  selector: 'app-return-request',
  templateUrl: './return-request.component.html',
  styleUrls: ['./return-request.component.css']
})
export class ReturnRequestComponent {
  

  userRentalAgreements: any[] = [];
  rentalDurationLeft:number=0;
  errorMessage:string='';
  successMessage:string ='';
  showError: boolean=false;

  constructor(private route: ActivatedRoute, private rentalService: UserService, private router: Router, private formBuilder: FormBuilder, public accountService: AccountService){}

  ngOnInit(): void {
    this.refreshUser();
    this.loadReturnRequests();
  }

  loadReturnRequests(): void {
    this.rentalService.getReturnRequestCars().subscribe(
      (agreements: any) => {
        if (agreements!=0) {
          
          this.userRentalAgreements = agreements.map((agreement: { rentalDuration: number; rentDate: string; returnDate: string;cost:number, userId:string }) => {
            let returnDate = new Date();
            let rentDate = new Date(agreement.rentDate);
            
            agreement.rentalDuration = this.calculateDaysDifference(rentDate,returnDate);
            this.rentalDurationLeft = agreement.rentalDuration;
            agreement.cost = this.calculateCurrentCost();
            return agreement;
          });
          
          console.log(this.userRentalAgreements);
        }
        else{
          this.successMessage='No data exists';
        this.showError = true;
          setTimeout(() => {
            this.showError = false;
            this.successMessage = '';
          }, 5000);
          console.log("No data exists");
        }
      },
      error => {
        this.errorMessage='Error fetching rental agreements:';
        this.showError = true;
          setTimeout(() => {
            this.showError = false;
            this.errorMessage = '';
          }, 5000);
          console.log("Error fetching rental agreements:");
        
        console.error('Error fetching rental agreements:', error);
      }
    );
  }


  AcceptRequest(carId:Number, userId:string, adminEmail:string){
  this.rentalService.acceptReturnRequests(carId,userId, adminEmail,true).subscribe(
    result => {
      this.successMessage='Return accepted';
      this.showError = true;
          setTimeout(() => {
            this.showError = false;
            this.successMessage = '';
          }, 5000);
      console.log('Return accepted:', result);
      this.router.routeReuseStrategy.shouldReuseRoute = () => false;
      this.router.onSameUrlNavigation = 'reload';
      this.router.navigate([this.router.url]);
    },
    error => {
      this.errorMessage='Error accepting return request:';
      this.showError = true;
          setTimeout(() => {
            this.showError = false;
            this.errorMessage = '';
          }, 5000);
      console.error('Error accepting return request:', error);
    }
  );
  }
  
  DenyRequest(carId:Number, userId:string, adminEmail:string){
    this.rentalService.acceptReturnRequests(carId,userId, adminEmail,false).subscribe(
      result => {
        this.successMessage='Return denied';
        this.showError = true;
          setTimeout(() => {
            this.showError = false;
            this.successMessage = '';
          }, 5000);
        console.log('Return denied:', result);
        this.router.routeReuseStrategy.shouldReuseRoute = () => false;
      this.router.onSameUrlNavigation = 'reload';
      this.router.navigate([this.router.url]);
      },
      error => {
        this.errorMessage='Error denying return request:';
        this.showError = true;
          setTimeout(() => {
            this.showError = false;
            this.errorMessage = '';
          }, 5000);
        console.error('Error denying return request:', error);
      }
    );
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

  calculateDaysDifference(rentDate: Date, returnDate: Date): number {
    const ONE_DAY_MS = 24 * 60 * 60 * 1000;

    var diff = Math.abs(returnDate.getTime() - rentDate.getTime());
    var diffDays = Math.ceil(diff / (1000 * 3600 * 24));

    return diffDays;
  }

  calculateCurrentCost(){
    const duration = this.rentalDurationLeft;
    return duration*1000;
  }


}
