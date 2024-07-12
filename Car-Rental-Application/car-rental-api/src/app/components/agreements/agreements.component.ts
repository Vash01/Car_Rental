import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AccountService } from '../account/account.service';
import { UserService } from '../dashboard/dashboard.service';

@Component({
  selector: 'app-agreements',
  templateUrl: './agreements.component.html',
  styleUrls: ['./agreements.component.css']
})
export class AgreementsComponent {

  userRentalAgreements: any[] = [];
  userEmail:string='';
  rentalDuration:number=0;

  

  constructor(
    private rentalService: UserService, 
    private route: ActivatedRoute,
    private accountService: AccountService
  ) { }

  ngOnInit(): void {
  this.refreshUser();
  this.route.params.subscribe(params => {
    this.userEmail = params['userEmail'];
    console.log('User Email:', this.userEmail);
    
  });
  
  this.loadUserRentalAgreements(this.userEmail);
  }

  ReturnRequest(carId:Number):void{
    this.rentalService.returnRequest(this.userEmail,carId).subscribe(
      result => {
        // Handle the result as needed
        console.log('Rental Request generated:', result);
      },
      error => {
        // Handle the error
        console.error('Error generating rental request:', error);
      }
    );
  }

  loadUserRentalAgreements(userEmail:string): void {
    this.rentalService.getUserRentalAgreements(userEmail).subscribe(
      (agreements: any) => {
        if (agreements) {
          // Assuming agreements is an array of rental agreements
          this.userRentalAgreements = agreements.map((agreement: { rentalDuration: number; rentDate: string; returnDate: string;cost:number }) => {
            // Calculate rental duration for each agreement
            agreement.rentalDuration = this.calculateDaysDifference(agreement.rentDate, agreement.returnDate);
            // agreement.cost = this.calculateTotalCost(agreement.rentalDuration,agreement.cost)
            return agreement;
          });
        
          console.log(this.userRentalAgreements);
        }
        else{
          console.log("No data exists");
        }
      },
      error => {
        console.error('Error fetching user rental agreements:', error);
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

  calculateDaysDifference(rentDate: string, returnDate: string): number {
    const ONE_DAY_MS = 24 * 60 * 60 * 1000; 
    const rentDateObj = new Date(rentDate);
    const returnDateObj = new Date(returnDate);
  
    // Calculate the difference in milliseconds
    const timeDifference = returnDateObj.getTime() - rentDateObj.getTime();
  
    // Calculate the difference in days
    const daysDifference = Math.floor(timeDifference / ONE_DAY_MS);
  
    return daysDifference;
  }
}
