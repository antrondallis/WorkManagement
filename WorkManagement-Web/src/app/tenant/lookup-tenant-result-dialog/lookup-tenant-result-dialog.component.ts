import { Component, OnInit, Inject } from '@angular/core';
import { Tenant } from 'src/app/_models/tenant';
import { WorkOrderCreateComponent } from 'src/app/work-order/work-order-create/work-order-create.component';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialog } from '@angular/material/dialog';
import { LookupTenantDialogComponent } from '../lookup-tenant-dialog/lookup-tenant-dialog.component';
import { TenantResultResponse } from 'src/app/_models/dto/tenantresultresponse';

@Component({
  selector: 'app-lookup-tenant-result-dialog',
  templateUrl: './lookup-tenant-result-dialog.component.html',
  styleUrls: ['./lookup-tenant-result-dialog.component.css']
})
export class LookupTenantResultDialogComponent implements OnInit {
  // tenants: Tenant[];
  constructor(public dialogRef: MatDialogRef<WorkOrderCreateComponent>,
              @Inject(MAT_DIALOG_DATA) public data: TenantResultResponse,
              private dialog: MatDialog) { }

  ngOnInit() {
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  searchDialog(): void {
    this.data.response = 'searchDialog';
    this.dialogRef.close(this.data);
  }

  selectTenant(tenantId: number): void {
    this.data.response = 'selected';
    this.data.selectedTenant = this.data.tenants.find(t => t.id === tenantId);
    this.dialogRef.close(this.data);
  }

}
