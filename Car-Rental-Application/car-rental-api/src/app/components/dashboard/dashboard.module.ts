import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UserDashboardComponent } from './user-dashboard/user-dashboard.component';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { CarListComponent } from '../car-list/car-list.component';
import { RentalAgreementComponent } from '../rental-agreement/rental-agreement.component';

@NgModule({
  declarations: [
    UserDashboardComponent,
    AdminDashboardComponent,
    CarListComponent,
  ],
  imports: [
    CommonModule,
    DashboardRoutingModule,
    FormsModule,
    ReactiveFormsModule,
  ]
 
})
export class AccountModule { }