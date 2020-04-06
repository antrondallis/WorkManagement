import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';
import { map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AlertifyService } from './alertify.service';

@Injectable({
    providedIn: 'root'
})

export class AuthService {
    baseUrl = environment.apiUrl + 'auth/';
    jwtHelper = new JwtHelperService();
    decodedToken: any;
    currentUser: User;

    constructor( private http: HttpClient, private alertify: AlertifyService) {}

    login(model: any) {
        console.log(model);
        return this.http.post(this.baseUrl + 'login', model)
            .pipe(
                map((response: any) => {
                    const user = response;
                    if (user) {
                        localStorage.setItem('token', user.token);
                        localStorage.setItem('user', JSON.stringify(user.user));
                        this.currentUser = user.user;
                        this.decodedToken = this.jwtHelper.decodeToken(user.token);
                    }
                })
            );
    }

    loggedIn() {
        const token = localStorage.getItem('token');
        return !this.jwtHelper.isTokenExpired(token);
    }
}
