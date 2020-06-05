import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { WorkOrderType } from 'src/app/_models/workordertype';
import { WorkOrderStatusCode } from 'src/app/_models/workorderstatuscode';
import { User } from 'src/app/_models/user';
import { AuthService } from 'src/app/_services/auth.service';
import { WorkOrderService } from 'src/app/_services/work-order.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { MatDialog } from '@angular/material/dialog';
import { LookupTenantDialogComponent } from 'src/app/tenant/lookup-tenant-dialog/lookup-tenant-dialog.component';
import { Tenant } from 'src/app/_models/tenant';
import { TenantService } from 'src/app/_services/tenant.service';
import { Formatter } from 'src/app/helpers/formatter';
import { LookupTenantResultDialogComponent } from 'src/app/tenant/lookup-tenant-result-dialog/lookup-tenant-result-dialog.component';
import { TenantResultResponse } from 'src/app/_models/dto/tenantresultresponse';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-work-order-create',
  templateUrl: './work-order-create.component.html',
  styleUrls: ['./work-order-create.component.css']
})
export class WorkOrderCreateComponent implements OnInit {
  workOrderCreateForm: FormGroup;
  workOrderTypes: WorkOrderType[];
  workOrderStatusCodes: WorkOrderStatusCode[];
  selectedTenant: Tenant;
  selectedTypeId = 0;
  selectedStatusId = 0;
  selectedUser: User;
  user: User;

  constructor(private authService: AuthService, private fb: FormBuilder, private workOrderService: WorkOrderService,
              private alertify: AlertifyService, private dialog: MatDialog, private tenantService: TenantService,
              private formatter: Formatter, private userService: UserService) { }

  ngOnInit() {
    this.GetWorkOrderTypes();
    this.GetWorkOrderStatusCodes();
    this.createWorkOrderForm();
  }

  createWorkOrderForm() {
    this.workOrderCreateForm = this.fb.group({
      workOrderType: [1],
      title: ['', Validators.required],
      description: ['', Validators.required],
      assignedTo: [''],
      requestFor: ['', Validators.required],
      submittedBy: [this.userService.getLoginUserId(), Validators.required]
    });
  }

  GetWorkOrderTypes() {
    this.workOrderService.getWorkOrderTypes().subscribe((types: WorkOrderType[]) => {
      this.workOrderTypes = types;
    }, error => {
      this.alertify.error(error);
    });
  }

  GetWorkOrderStatusCodes() {
    this.workOrderService.getWorkOrderStatusCodes().subscribe((codes: WorkOrderStatusCode[]) => {
      this.workOrderStatusCodes = codes;
    }, error => {
      this.alertify.error(error);
    });
  }

  lookUpTenant() {
    let resultList: Tenant[];
    let searchParam: Tenant;
    // open dialog box for tenant search
    const dialogRef = this.dialog.open(LookupTenantDialogComponent, {
      width: '700px',
      height: '400px',
      data: { id: 0, firstName: '', lastName: '', apartmentNo: '' }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        searchParam = result;
        this.formatter.formatTenantSearchParam(searchParam);

        // make a web service request based on the search criteria
        this.tenantService.lookUpTenant(searchParam).subscribe((resultSet: Tenant[]) => {
          resultList = resultSet;
          this.showSearchResults(resultList);
        }, error => {
          this.alertify.error(error.message);
          console.log(error);
        });
      }
    });
  }

  showSearchResults(results: Tenant[]) {
    // tslint:disable-next-line: prefer-const
    let passTenant: Tenant;
    const resultDialog = this.dialog.open(LookupTenantResultDialogComponent, {
      width: '740px',
      height: '500px',
      data: { tenants: results, response: '', selectedTenant: passTenant }
    });

    // search result selected and display in tenant input boxes
    // or return to tenant search form
    resultDialog.afterClosed().subscribe((result: TenantResultResponse) => {
      if (result) {
        if (result.response === 'searchDialog') {
          this.lookUpTenant();
        } else if (result.response === 'selected') {
          this.selectedTenant = result.selectedTenant;
          this.workOrderCreateForm.patchValue({requestFor: this.selectedTenant.id});
        }
      } else {
        console.log('no result');
      }
    });
  }

  assignedToLoggedInUser() {
    const user: User = this.userService.getLoginUser();
    this.selectedUser = user;
    this.workOrderCreateForm.patchValue({requestFor: user.id});
  }

  submitWorkOrder() {
    const workOrderCreate = Object.assign({}, this.workOrderCreateForm.value);
    console.log('submitworkorder clicked');
    console.log(workOrderCreate);
  }

}
