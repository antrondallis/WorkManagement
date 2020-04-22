import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/_services/auth.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { Router } from '@angular/router';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-admin-layout',
  templateUrl: './admin-layout.component.html',
  styleUrls: ['./admin-layout.component.css']
})
export class AdminLayoutComponent implements OnInit {
  userFullName: string;

  constructor(public authService: AuthService, private alertify: AlertifyService,
              private userService: UserService, private router: Router) { }

  ngOnInit() {
    // grab the users full name to print on the nav bar
    const user = this.userService.getLoginUser();
    this.userFullName = user.firstName + ' ' + user.lastName;
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.authService.decodedToken = null;
    this.authService.currentUser = null;
    this.alertify.message('logged out');
    this.router.navigate(['/login']);
  }

}
