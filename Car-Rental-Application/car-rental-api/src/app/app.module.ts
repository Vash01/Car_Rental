import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PageNotFoundComponent } from './components/errors/page-not-found/page-not-found.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor } from './interceptor/jwt.interceptor';
import { RentalAgreementComponent } from './components/rental-agreement/rental-agreement.component';
import { EditRentalAgreementComponent } from './components/edit-rental-agreement/edit-rental-agreement/edit-rental-agreement.component';
import { AgreementsComponent } from './components/agreements/agreements.component';
import { ReturnRequestComponent } from './components/return-request/return-request.component';
import { AddCarRecordComponent } from './components/add-car-record/add-car-record.component';
@NgModule({
  declarations: [
    AppComponent,
    PageNotFoundComponent,
    RentalAgreementComponent,
    EditRentalAgreementComponent,
    AgreementsComponent,
    ReturnRequestComponent,
    AddCarRecordComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass:JwtInterceptor, multi:true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
