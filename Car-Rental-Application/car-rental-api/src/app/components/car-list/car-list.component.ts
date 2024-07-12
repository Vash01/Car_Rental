import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { NavigationExtras, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Car } from 'src/app/models/Car';
import { environment } from 'src/environments/environment.development';
import { CarService } from './car.service';

@Component({
  selector: 'app-car-list',
  templateUrl: './car-list.component.html',
  styleUrls: ['./car-list.component.css']
})
export class CarListComponent {

  cars: any[] = [];
  searchForm: FormGroup;
  selectedCar: any =null;
  rentalDuration: number = 10;
  totalCost: number = 10000;

  constructor(private carService: CarService, private fb: FormBuilder, private router: Router) {
    this.searchForm = this.fb.group({
      maker: [''],
      modelYear: [],
      // rentalPrice: [''],
    });
  }

  ngOnInit() {
    this.loadCars();
  }

  loadCars() {
    this.carService.getCars().subscribe(
      (data) => {
        console.log("car data:",data);
        this.cars = data;
      },
      (error) => {
        console.error('Error fetching cars:', error);
        // Handle error as needed
      }
    );
  }

  searchCars() {
    const query = this.searchForm.value;
    this.carService.searchCars(query).subscribe(
      (data) => {
        this.cars = data;
      },
      (error) => {
        console.error('Error searching cars:', error);
      }
    );
  }

  showRentalAgreement(car: Car):void{
    if(car){
      this.selectedCar=car;
      const navigationExtras: NavigationExtras = {
        queryParams: {
          param1: encodeURIComponent(JSON.stringify(this.selectedCar)),
          param2: this.rentalDuration,
          param3: this.totalCost,
        }
      };
      
      this.router.navigate(['/rental-agreement'],navigationExtras);
    }
    else{
      console.error('Invalid car details:', car);
    }
  }
}
