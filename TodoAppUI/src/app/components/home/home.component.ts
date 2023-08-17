import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user.model';
import { AuthService } from 'src/app/services/auth.service';
import { HomeService } from 'src/app/services/home.service';

@Component({
  selector: 'app-client-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class ClientHomeComponent implements OnInit{
  constructor(private AuthService: AuthService, private HomeService: HomeService){}

  userValue?: User;
  clientName?: string;

  ngOnInit(): void {
    this.establishAUser();
  }
  establishAUser(): void {
    this.HomeService.getHomePage();
    this.userValue = this.AuthService.userValue!;
    console.log(this.userValue)
    this.clientName = `${this.userValue.firstName} ${this.userValue.lastName}`;
  }

}
