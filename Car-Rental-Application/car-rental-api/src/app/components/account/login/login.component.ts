import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  constructor( private router: Router, private formBuilder: FormBuilder, private accountService: AccountService, private activatedRoute: ActivatedRoute){

  }
  loginForm: FormGroup = new FormGroup({});
  errorMessage: string = '';
  showError: boolean=false;
  submitted: boolean = false;
  returnUrl:string | null = null;

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm(){
    this.loginForm = new FormGroup({
      email: new FormControl("admin@admin.com", [Validators.required, Validators.email]),
      password: new FormControl("", [Validators.required, Validators.minLength(1)]),
    });
  }

  login():void{
    if(this.isFormValid()){
      this.submitted = true;
      console.log(this.loginForm.value);
      this.accountService.login(this.loginForm.value).subscribe({
        next: (response:any)=>{
          if(this.returnUrl){
            this.router.navigateByUrl(this.returnUrl);
          }
          else{
            if (this.loginForm.value.email && this.loginForm.value.email.endsWith('@admin.com')) {
              this.router.navigateByUrl('/dashboard/admin');
            } else {
              this.router.navigateByUrl('/dashboard/user');
            }
          // this.router.navigateByUrl('/dashboard/');
          // console.log(response);
          }
        },
        error:error=>{
          if (error.status === 0) {
            this.errorMessage = 'Email or password is incorrect.';
          } else {
            this.errorMessage = 'Login failed. Please try again.';
          }
          this.showError = true;
          setTimeout(() => {
            this.showError = false;
            this.errorMessage = '';
          }, 5000);
          this.activatedRoute.queryParamMap.subscribe({
            next:(params: any)=>{
              if(params){
                this.returnUrl = params.get('returnUrl');
              }
            }
          })
        }
      });
    }
   else {
    this.errorMessage = 'Please fill in all required fields and fix validation errors.';
    this.showError = true;
        setTimeout(() => {
          this.showError = false;
          this.errorMessage = '';
        }, 5000);
    }
  }

  isFormValid(): boolean {
    // Check the validity of the entire form
    return this.loginForm.valid;
  }
}
