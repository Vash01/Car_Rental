import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from '../../account/account.service';
import { CarService } from '../../car-list/car.service';
import { UserService } from '../dashboard.service';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent {


  userRentalAgreements: any[] = [];
  userEmail:string='';
  rentalDuration:number=0;
  errorMessage:string='';
  successMessage:string ='';
  showError: boolean=false;


  constructor( private rentalService: UserService, private router: Router, private formBuilder: FormBuilder, public accountService: AccountService, public carService:CarService){}

  ngOnInit(): void {
    this.refreshUser();
    this.loadRentalAgreements();
  }
  
  loadRentalAgreements(): void {
    this.rentalService.getAllRentalAgreements().subscribe(
      (agreements: any) => {
        if (agreements !=0) {
          this.userRentalAgreements = agreements.map((agreement: { rentalDuration: number; rentDate: string; returnDate: string;cost:number }) => {
            
            agreement.rentalDuration = this.calculateDaysDifference(agreement.rentDate, agreement.returnDate);
            // agreement.cost = this.calculateTotalCost(agreement.rentalDuration,agreement.cost)
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
        this.errorMessage='Error fetching Car details:';
        this.showError = true;
          setTimeout(() => {
            this.showError = false;
            this.errorMessage = '';
          }, 5000);
        console.error('Error fetching Car details:', error);
      }
    );
  }

  requestReturns():void{
    this.router.navigateByUrl('/return-requests');
  }

  deleteRecord(carId:any){
    this.carService.deleteCarRecord(carId).subscribe(
    result => {
      this.successMessage='Record Deleted';
      this.showError = true;
        setTimeout(() => {
          this.showError = false;
          this.successMessage = '';
        }, 5000);
      console.log('record deleted:', result);
      this.router.routeReuseStrategy.shouldReuseRoute = () => false;
    this.router.onSameUrlNavigation = 'reload';
    this.router.navigate([this.router.url]);
    },
    error => {
      this.errorMessage='Error deleting record:';
      this.showError = true;
        setTimeout(() => {
          this.showError = false;
          this.errorMessage = '';
        }, 5000);
      console.error('Error deleting record:', error);
    }
  );
  }

  AddCar(){
    this.router.navigateByUrl('/add-car-record');
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
