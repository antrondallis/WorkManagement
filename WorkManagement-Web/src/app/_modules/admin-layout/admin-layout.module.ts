import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from 'src/app/home/home.component';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { AngularFontAwesomeModule } from 'angular-font-awesome';
import { JwtModule } from '@auth0/angular-jwt';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldControl, MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';

import { AdminLayoutRoutes } from './admin-layout.routing';
import { AdminLayoutComponent } from './admin-layout.component';
import { WorkOrderDetailComponent } from 'src/app/work-order/work-order-detail/work-order-detail.component';
import { WorkOrderEditComponent } from 'src/app/work-order/work-order-edit/work-order-edit.component';
import { WorkOrderNotesAddDialogComponent } from 'src/app/work-order/work-order-notes-add-dialog/work-order-notes-add-dialog.component';

export function tokenGetter() {
  return localStorage.getItem('token');
}

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(AdminLayoutRoutes),
    FormsModule,
    AngularFontAwesomeModule,
    MatInputModule,
    MatFormFieldModule,
    MatSelectModule,
    MatDialogModule,
    MatButtonModule
  ],
  declarations: [
    HomeComponent,
    WorkOrderDetailComponent,
    WorkOrderEditComponent,
    WorkOrderNotesAddDialogComponent
  ],
  entryComponents : [
    WorkOrderNotesAddDialogComponent
  ]
})
export class AdminLayoutModule { }
