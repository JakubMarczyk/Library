import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UsersService } from 'src/app/services/users.service';
import { Login } from '../../models/login.model';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent {
  loginData: Login = {
    email: "",
    password: ""
  }
  errorMessage?: string;
  constructor(private usersService: UsersService, private router: Router) { }

  login() {
    this.usersService.login(this.loginData).subscribe(response => {
      if (this.usersService.getUserRole() != 'Admin') {
        this.router.navigate(['/']);
      } else
        this.router.navigate(['/books']);
      localStorage.setItem('token', response.token);
    },
      error =>{
        this.errorMessage = 'Nieprawidłowe dane logowania. Spróbuj ponownie.';
      });
  }
}
