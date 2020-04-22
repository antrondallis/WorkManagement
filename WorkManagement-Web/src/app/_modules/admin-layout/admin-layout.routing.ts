import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from 'src/app/home/home.component';
import { WorkOrderDetailComponent } from 'src/app/work-order/work-order-detail/work-order-detail.component';

export const AdminLayoutRoutes: Routes = [
  { path: 'dashboard', component: HomeComponent},
  { path: 'work-order-detail/:id', component: WorkOrderDetailComponent}
];
