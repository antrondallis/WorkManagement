import { Component, OnInit, Inject } from '@angular/core';
import { Tenant } from 'src/app/_models/tenant';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { WorkOrderCreateComponent } from 'src/app/work-order/work-order-create/work-order-create.component';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-lookup-tenant-dialog',
  templateUrl: './lookup-tenant-dialog.component.html',
  styleUrls: ['./lookup-tenant-dialog.component.css']
})
export class LookupTenantDialogComponent implements OnInit {
  tenant: Tenant;
  loopUpTenantForm: FormGroup;
  constructor(public dialogRef: MatDialogRef<WorkOrderCreateComponent>,
              @Inject(MAT_DIALOG_DATA) public data: Tenant) { }

  ngOnInit() {
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  lookUpTenant() {

  }

}
