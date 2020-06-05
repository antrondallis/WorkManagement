import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from 'src/app/home/home.component';
import { WorkOrderDetailComponent } from 'src/app/work-order/work-order-detail/work-order-detail.component';
import { WorkOrderEditComponent } from 'src/app/work-order/work-order-edit/work-order-edit.component';
import { WorkOrderAllComponent } from 'src/app/work-order/work-order-all/work-order-all.component';
import { WorkOrderCreateComponent } from 'src/app/work-order/work-order-create/work-order-create.component';

export const AdminLayoutRoutes: Routes = [
  { path: 'dashboard', component: HomeComponent},
  { path: 'work-order-detail/:id', component: WorkOrderDetailComponent},
  { path: 'work-order-edit/:id', component: WorkOrderEditComponent},
  { path: 'work-order-all', component: WorkOrderAllComponent},
  { path: 'work-order-new', component: WorkOrderCreateComponent}
];
