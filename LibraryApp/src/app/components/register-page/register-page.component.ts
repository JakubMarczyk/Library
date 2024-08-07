import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UsersService } from 'src/app/services/users.service';
import { Register } from '../../models/register.model';

@Component({
  selector: 'app-register-page',
  templateUrl: './register-page.component.html',
  styleUrls: ['./register-page.component.css']
})
export class RegisterPageComponent {
  registerData: Register = {
    nickname: "",
    email: "",
    password: "",
  }
  
  errorMessage?: string;
  constructor(private usersService: UsersService, private router: Router) { }

  register(): void {
    this.usersService.register(this.registerData).subscribe({
      next: (response) => {
        this.router.navigate(['/login']);
      },
      error: (error) => {
        this.errorMessage = error.message;
      }
    });
  }
}
