import { CanActivate, Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Injectable({
    providedIn: 'root'
})

export class LoginGuard implements CanActivate {
    constructor(private router: Router, private authService: AuthService) {}

    canActivate(): boolean {
        if (this.authService.loggedIn()) {
            this.router.navigate(['/home']);
            return true;
        }

        return false;
    }

}
