import { Component } from '@angular/core';
import { ActivatedRoute, NavigationExtras, Params, Router } from '@angular/router';
import { Car } from 'src/app/models/Car';
import { User } from 'src/app/models/User';
import { AccountService } from '../account/account.service';
import { UserService } from '../dashboard/dashboard.service';

@Component({
  selector: 'app-rental-agreement',
  templateUrl: './rental-agreement.component.html',
  styleUrls: ['./rental-agreement.component.css']
})
export class RentalAgreementComponent{

  selectedCar: Car= {
    id:0,
    maker: 'hey',
    carName: '',
    colorName: '',
    modelYear: 0,
    description:''
  } ;

  car: Car = {
    id:0,
    maker: '',
    carName: '',
    colorName: '',
    modelYear: 0,
    description: ''
  };


  rentalDuration: number = 0;
  totalCost: number = 0;

  user: User = {
    // Initialize user properties as needed
    name: '',
    email: '',
    jwt:''
  };

  constructor(
    private route: ActivatedRoute,
    public accountService: AccountService,
    private userService: UserService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.refreshUser();

    this.route.queryParams.subscribe((queryParams) => {

      console.log('Raw queryParams:', queryParams);
      const param1 = queryParams['param1'];
    //  console.log(param1);
      if (param1) {
        try {

          const decodedParam1 = decodeURIComponent(param1);
          console.log(decodedParam1);

        // Parse the JSON string into an object
         this.selectedCar = JSON.parse(decodedParam1);
         console.log('Decoded and Parsed param1:', this.selectedCar);

        } catch (error) {
          console.error('Error parsing param1 as JSON:', error);
          // Handle the error or set default values
        }
      } else {
        console.error('param1 is null or undefined');
        // Handle the case when 'param1' is not present
      }
    
      this.rentalDuration = Number(queryParams['param2']) || 0;
      this.totalCost = Number(queryParams['param3']) || 0;

      // console.log('Selected Car:', this.selectedCar);
    });
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

  acceptRentalAgreement(userEmail: string, CarId: Number): void {
    console.log("total cost before submiting", this.rentalDuration);
    this.userService.acceptRentalAgreement(userEmail, CarId, this.rentalDuration).subscribe(
      result => {
        console.log('Rental agreement accepted:', result);
      },
      error => {
        console.error('Error accepting rental agreement:', error);
      }
    );
  }


  editRentalAgreement(): void {
    const navigationExtras: NavigationExtras = {
      queryParams: {
        param1: encodeURIComponent(JSON.stringify(this.selectedCar)),
        param2: this.rentalDuration,
        param3: this.totalCost,
      }
    };
    this.router.navigate(['/edit-rental-agreement'], navigationExtras);
  }

}
