import { Component, OnInit } from '@angular/core';
import { UsersService } from '../../services/users.service';
import { User } from '../../models/user.model';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  user:any;
  userId = "";
  isLogged: boolean = false;
  currentPassword: string = "";
  newPassword: string = "";
  deletePassword: string = "";
  constructor(private usersService: UsersService) {

  }

  ngOnInit() : void{
    this.isUserLogged()
  }



  isUserLogged() {
    this.isLogged = this.usersService.isLoggedInUser();
    if (this.isLogged) {
      this.userId = this.usersService.getUserId()
      this.getUser();
    }
  }

  getUser() {
    this.usersService.getUser(this.userId).subscribe({
      next: (user) => {
        this.user = user
      },
      error: (response) => {
        console.log(response)
      }
    });
  }

  changePassword() {
    this.usersService.changePassword(this.userId, this.currentPassword, this.newPassword).subscribe({
        next: (user) => {
        this.user = user
        this.usersService.logout();
        },
        error: (response) => {
          console.log(response)
        }
      });
    }

  deleteAccount() {
    this.usersService.deleteAccount(this.userId, this.deletePassword).subscribe({
        next: (user) => {
        this.user = user
        this.usersService.logout();
        },
        error: (response) => {
          console.log(response)
        }
      });
    }
  
}
