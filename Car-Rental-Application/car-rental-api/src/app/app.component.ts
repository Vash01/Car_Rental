import { Component, OnInit } from '@angular/core';
import { AccountService } from './components/account/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  constructor(private accountService: AccountService){}
  title = 'car-rental-api';
}
