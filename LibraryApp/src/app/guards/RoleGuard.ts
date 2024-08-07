import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { UsersService } from '../services/users.service';

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {

  constructor(private usersService: UsersService, private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    console.log('RoleGuard: canActivate invoked');
    console.log('Requested URL:', state.url);

    if (state.url === '/login' || state.url === '/register') {
      console.log('Inside canActivate: URL is login/register');
      if (this.usersService.isLoggedInUser()) {
        console.log('Inside canActivate: User is logged in, redirecting to /');
        this.router.navigate(['/']);
        return false;
      } else {
        console.log('Inside canActivate: User is not logged in');
        return true;
      }
    } else {
      console.log('Inside canActivate: URL is not login/register');
      if (this.usersService.isLoggedInUser()) {
        console.log('Inside canActivate: User is logged in');
        const userRole = this.usersService.getUserRole();
        console.log('User role:', userRole);
        if (userRole === 'Admin') {
          console.log('Inside canActivate: User is admin, allowing access');
          return true;
        } else {
          console.log('Inside canActivate: User is not admin, redirecting to /page-not-found');
          this.redirectToNotFound();
          return false;
        }
      } else {
        console.log('Inside canActivate: User is not logged in, redirecting to /login');
        this.redirectToLogin();
        return false;
      }
    }
  }

  private redirectToNotFound(): void {
    console.log('Redirecting to /page-not-found');
    this.router.navigate(['/page-not-found']);
  }

  private redirectToLogin(): void {
    console.log('Redirecting to /login');
    this.router.navigate(['/login']);
  }
}
