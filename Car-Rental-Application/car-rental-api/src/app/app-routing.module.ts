import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddCarRecordComponent } from './components/add-car-record/add-car-record.component';
import { AgreementsComponent } from './components/agreements/agreements.component';
import { EditRentalAgreementComponent } from './components/edit-rental-agreement/edit-rental-agreement/edit-rental-agreement.component';
import { PageNotFoundComponent } from './components/errors/page-not-found/page-not-found.component';
import { RentalAgreementComponent } from './components/rental-agreement/rental-agreement.component';
import { ReturnRequestComponent } from './components/return-request/return-request.component';
import { AuthorizationGuard } from './guards/authorization.guard';

const routes: Routes = [

  { path: '', redirectTo: '/account/login', pathMatch: 'full' },

  // {path:'dashboard',
  // runGuardsAndResolvers: 'always',
  // canActivate:[AuthorizationGuard],
  // children:[
  //   {path:'',loadChildren:()=>import ('../app/components/dashboard/dashboard.module').then(module=> module.AccountModule)}
  // ]
  // },

  {path:'account', loadChildren:()=>import ('../app/components/account/account.module').then(module=> module.AccountModule)},

  {path:'dashboard',loadChildren:()=>import ('../app/components/dashboard/dashboard.module').then(module=> module.AccountModule)},

  
{path:'rental-agreement',
component: RentalAgreementComponent}
,
{path:'edit-rental-agreement',
component: EditRentalAgreementComponent},

{path:'agreement/:userEmail',
component: AgreementsComponent},

{path:'return-requests',
component: ReturnRequestComponent},
{
  path:'add-car-record',
  component: AddCarRecordComponent
},
{
  path:'**',
  component:PageNotFoundComponent
}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
