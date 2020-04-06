import { Injectable } from '@angular/core';
import { User } from '../_models/user';
import { AlertifyService } from './alertify.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  currentUser: User;

  constructor(private alertify: AlertifyService) { }

  getLoginUser() {
    return JSON.parse(localStorage.getItem('user'));
  }

}
