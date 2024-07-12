import { ChangeDetectorRef, Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Car } from 'src/app/models/Car';
import { AccountService } from '../account/account.service';
import { CarService } from '../car-list/car.service';
import { UserService } from '../dashboard/dashboard.service';

@Component({
  selector: 'app-add-car-record',
  templateUrl: './add-car-record.component.html',
  styleUrls: ['./add-car-record.component.css']
})
export class AddCarRecordComponent {

  rentalForm: FormGroup;

  car: Car = {
    id:0,
    maker: '',
    carName: '',
    colorName: '',
    modelYear: 0,
    description: ''
  };
  
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    public accountService: AccountService,
    public userService: UserService,
    private cdr: ChangeDetectorRef ,
    public carService: CarService
  ) {
    this.rentalForm = this.fb.group({
      car: this.fb.group({
        maker: ['', [Validators.required, Validators.minLength(2)]],
        modelYear: ['', [Validators.required, Validators.minLength(4)]],
        carName: ['', [Validators.required, Validators.minLength(1)]],
        description: ['', [Validators.required, Validators.min(4)]],
        colorName: ['', [Validators.required]],
      }),
      cost: [1000],
      customerName: ['', [Validators.required, Validators.minLength(2)]],
    });
  }

  ngOnInit(): void {
    this.refreshUser();}

submitForm(userEmail:any) {
this.carService.addCarRecord(this.rentalForm.value, userEmail).subscribe(
  (data) => {
    console.log('Car added succesfully');
  },
  (error) => {
    console.error('Error searching cars:', error);
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

}
