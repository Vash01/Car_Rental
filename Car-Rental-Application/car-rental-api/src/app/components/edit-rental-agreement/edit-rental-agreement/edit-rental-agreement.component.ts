import { ChangeDetectorRef, Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, NavigationExtras, Router } from '@angular/router';
import { min } from 'rxjs';
import { Car } from 'src/app/models/Car';
import { User } from 'src/app/models/User';
import { AccountService } from '../../account/account.service';
import { UserService } from '../../dashboard/dashboard.service';

@Component({
  selector: 'app-edit-rental-agreement',
  templateUrl: './edit-rental-agreement.component.html',
  styleUrls: ['./edit-rental-agreement.component.css']
})
export class EditRentalAgreementComponent{

  rentalForm: FormGroup;
  

  selectedCar: Car = {
    id:0,
    maker: 'hey',
    modelYear: 0,
    carName: '',
    colorName: '',
    description:''
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
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    public accountService: AccountService,
    public userService: UserService,
    private cdr: ChangeDetectorRef 
  ) {
    this.rentalForm = this.fb.group({
      rentalDuration: [this.rentalDuration,[Validators.required, Validators.min(2)]], // Assuming rentalDuration is required
    });

    // Subscribe to changes in rentalDuration to recalculate totalCost
    this.rentalForm.get('rentalDuration')?.valueChanges.subscribe(response => {
      this.calculateTotalCost();
      this.cdr.detectChanges();
    });
  }

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
    
      this.rentalDuration = Number(queryParams['param2']) ;
      this.rentalForm.get('rentalDuration')?.setValue(this.rentalDuration);
      // console.log("rental duration",this.rentalDuration);
      this.totalCost = Number(queryParams['param3']) || 0;

      // console.log('Selected Car:', this.selectedCar);
    });
  }

  calculateTotalCost(): void {
    const duration = this.rentalForm.get('rentalDuration')?.value;
    if (typeof duration === 'number' && !isNaN(duration)) {
      this.totalCost = duration * 1000;
    } else {
      // Handle the case where duration is not a valid number
      this.totalCost = 0;
    }
      
  }
  
  rentalAgreement():void{
    const navigationExtras: NavigationExtras = {
      queryParams: {
        param1: encodeURIComponent(JSON.stringify(this.selectedCar)),
        param2: this.rentalDuration,
        param3: this.totalCost,
      }
    };
  // console.log("checking selected car in edit",this.selectedCar);
    this.router.navigate(['/rental-agreement'],navigationExtras);
  }

  submitForm(): void {
    // console.log("new rental duration:", this.rentalForm.value);
    const duration = this.rentalForm.get('rentalDuration')?.value;
    const navigationExtras: NavigationExtras = {
      queryParams: {
        param1: encodeURIComponent(JSON.stringify(this.selectedCar)),
        param2: duration,
        param3: this.totalCost,
      }
    };
  // console.log("checking selected car in edit",this.selectedCar);
    this.router.navigate(['/rental-agreement'],navigationExtras);
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
}
