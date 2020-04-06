import { Component, OnInit } from '@angular/core';
import { LoginUser } from 'src/app/_models/loginuser';
import { AuthService } from 'src/app/_services/auth.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { Router } from '@angular/router';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  model: any = {};
  loginForm: FormGroup;

  constructor(private authService: AuthService, private alertify: AlertifyService,
              private router: Router) {
                if (authService.loggedIn()) {
                  router.navigate(['/dashboard']);
                }
              }

  ngOnInit() {
  }

  login() {
    this.authService.login(this.model).subscribe(
      next => {
        this.alertify.success('Logged in successfully');
      }, error => {
        this.alertify.error(error.error);
      }, () => {
        this.router.navigate(['/dashboard']);
      }
    );
  }

}
