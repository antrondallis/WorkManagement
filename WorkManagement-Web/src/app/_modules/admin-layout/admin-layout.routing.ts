import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from 'src/app/home/home.component';

export const AdminLayoutRoutes: Routes = [
  { path: 'dashboard', component: HomeComponent}
];
