import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { take } from 'rxjs';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {

  constructor( private router: Router, private formBuilder: FormBuilder, private accountService: AccountService){

    this.accountService.user$.pipe(take(1)).subscribe({
      next: user=>{
        if(user){
          this.router.navigateByUrl('/dashboard');
        }
  }});
  }
  RegisterForm: FormGroup = new FormGroup({});
  errorMessage: string = '';
  showError: boolean=false;
  submitted: boolean = false;

  ngOnInit():void{
    this.initializeForm();
  } 

  initializeForm(){
    this.RegisterForm = this.formBuilder.group({
      name: new FormControl("", [Validators.required,Validators.pattern('[a-zA-Z]*'), Validators.minLength(3)]),
      email: new FormControl("", [Validators.required, Validators.email]),
      password: new FormControl("", [Validators.required, Validators.minLength(6),Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$/),]),
    })
  }  

  register():void{
    if(this.isFormValid()){
      this.submitted = true;
      console.log(this.RegisterForm.value);
      this.accountService.register(this.RegisterForm.value).subscribe({
        next: (response:any)=>{
          
          this.router.navigateByUrl('/account/register');
          console.log(response);
        },
        error:error=>{
          if (error.status === 0) {
            this.errorMessage = 'Email already exists. Please use a different email address.';
          } else {
            this.errorMessage = 'Registration failed. Please try again.';
          }
          this.showError = true;
          setTimeout(() => {
            this.showError = false;
            this.errorMessage = '';
          }, 5000);
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
    return this.RegisterForm.valid;
  }
}
