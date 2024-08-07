import { Component } from '@angular/core';
import { UsersService } from './services/users.service';
import { jwtDecode as jwt_decode } from "jwt-decode";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Library';
  constructor(private usersService: UsersService) { }

  ngOnInit() {
    const token = localStorage.getItem('token');
    if (token) {
      this.usersService.setLoggedIn(true);
    }
  }
}
