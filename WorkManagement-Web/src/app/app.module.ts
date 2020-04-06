import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { ValueComponent } from './value/value.component';
import { HomeComponent } from './home/home.component';
import { appRoutes, AppRoutingModule } from './routes';
import { AuthService } from './_services/auth.service';
import { LoginModule } from './_modules/login/login.module';
import { AdminLayoutComponent } from './_modules/admin-layout/admin-layout.component';
import { AngularFontAwesomeModule } from 'angular-font-awesome';

@NgModule({
   declarations: [
      AppComponent,
      ValueComponent,
      AdminLayoutComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      RouterModule,
      FormsModule,
      ReactiveFormsModule,
      LoginModule,
      AppRoutingModule,
      AngularFontAwesomeModule
   ],
   providers: [
      AuthService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
