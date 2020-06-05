import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from 'src/app/home/home.component';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AngularFontAwesomeModule } from 'angular-font-awesome';
import { JwtModule } from '@auth0/angular-jwt';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldControl, MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';

import { AdminLayoutRoutes } from './admin-layout.routing';
import { AdminLayoutComponent } from './admin-layout.component';
import { WorkOrderDetailComponent } from 'src/app/work-order/work-order-detail/work-order-detail.component';
import { WorkOrderEditComponent } from 'src/app/work-order/work-order-edit/work-order-edit.component';
import { WorkOrderNotesAddDialogComponent } from 'src/app/work-order/work-order-notes-add-dialog/work-order-notes-add-dialog.component';
import { WorkOrderConfirmCloseComponent } from 'src/app/work-order/work-order-confirm-close/work-order-confirm-close.component';
import { WorkOrderAllComponent } from 'src/app/work-order/work-order-all/work-order-all.component';
import { WorkOrderCreateComponent } from 'src/app/work-order/work-order-create/work-order-create.component';
import { LookupTenantComponent } from 'src/app/tenant/lookup-tenant/lookup-tenant.component';
import { LookupTenantDialogComponent } from 'src/app/tenant/lookup-tenant-dialog/lookup-tenant-dialog.component';
import { LookupTenantResultDialogComponent } from 'src/app/tenant/lookup-tenant-result-dialog/lookup-tenant-result-dialog.component';

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
    MatButtonModule,
    MatMenuModule,
    ReactiveFormsModule
  ],
  declarations: [
    HomeComponent,
    WorkOrderDetailComponent,
    WorkOrderEditComponent,
    WorkOrderNotesAddDialogComponent,
    WorkOrderConfirmCloseComponent,
    WorkOrderAllComponent,
    WorkOrderCreateComponent,
    LookupTenantDialogComponent,
    LookupTenantResultDialogComponent
  ],
  entryComponents : [
    WorkOrderNotesAddDialogComponent,
    WorkOrderConfirmCloseComponent,
    LookupTenantDialogComponent,
    LookupTenantResultDialogComponent
  ]
})
export class AdminLayoutModule { }
