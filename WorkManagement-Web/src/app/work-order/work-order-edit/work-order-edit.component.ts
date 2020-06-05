import { Component, OnInit, ViewChild, NgZone, Inject } from '@angular/core';
import { CdkTextareaAutosize } from '@angular/cdk/text-field';
import { ActivatedRoute } from '@angular/router';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { FormGroup } from '@angular/forms';
import {take} from 'rxjs/operators';

import { WorkOrder } from 'src/app/_models/workorder';
import { WorkOrderService } from 'src/app/_services/work-order.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { WorkOrderType } from 'src/app/_models/workordertype';
import { WorkOrderForEdit } from 'src/app/_models/workorderforedit';
import { WorkOrderStatusCode } from 'src/app/_models/workorderstatuscode';
import { WorkOrderNotesAddDialogComponent } from '../work-order-notes-add-dialog/work-order-notes-add-dialog.component';
import { WorkOrderNote } from 'src/app/_models/workordernote';
import { UserService } from 'src/app/_services/user.service';
import { WorkOrderUpdateDto } from 'src/app/_models/dto/workorderupdatedto';
import { WorkOrderConfirmCloseComponent } from '../work-order-confirm-close/work-order-confirm-close.component';
import { Formatter } from 'src/app/helpers/formatter';
import { User } from 'src/app/_models/user';
import { WorkOrderAssignDto } from 'src/app/_models/dto/updateworkorderassigndto';

@Component({
  selector: 'app-work-order-edit',
  templateUrl: './work-order-edit.component.html',
  styleUrls: ['./work-order-edit.component.css']
})
export class WorkOrderEditComponent implements OnInit {

  workOrderId: number;
  workOrder: WorkOrder;
  workOrderForEdit: WorkOrder;
  workOrderTypes: WorkOrderType[];
  workOrderStatusCodes: WorkOrderStatusCode[];
  selectedTypeId = 0;
  selectedStatusId = 0;
  closed: boolean;
  user: User;

  editForm: FormGroup;

  constructor(private route: ActivatedRoute, private workOrderService: WorkOrderService,
              private alertify: AlertifyService, private ngZone: NgZone, private dialog: MatDialog,
              private userService: UserService, private formatter: Formatter) { }

  @ViewChild('autosize', {static: false}) autosize: CdkTextareaAutosize;

  ngOnInit() {
    this.route.params.subscribe(p => {
      this.workOrderId = p.id;
    });

    this.GetByIdForEdit();
    this.GetWorkOrderTypes();
    this.GetWorkOrderStatusCodes();
  }

  GetByIdForEdit() {
    this.workOrderService.getByIdForEdit(this.workOrderId).subscribe((workOrder: WorkOrder) => {
      this.workOrder = workOrder;
      this.selectedTypeId = this.workOrder.workOrderType.id;
      this.formatter.formatAssignName(this.workOrder.assignedTo);
      this.selectedStatusId = this.workOrder.status.id;
    }, error => {
      this.alertify.error(error);
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

  openNotesDialog(): void {
    const dialogRef = this.dialog.open(WorkOrderNotesAddDialogComponent, {
      width: '700px',
      height: '300px',
      data: {workOrderId: 0, note: '', createdBy: ''}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        const noteToAdd: WorkOrderNote = {
          id: 0,
          workOrderId:  this.workOrderId,
          note: result,
          createDate: new Date(),
          createdBy: this.userService.getLoginUser()
        };
        this.workOrderService.addWorkOrderNote(noteToAdd).subscribe((httpStatusCode: number) => {
          if (!httpStatusCode) {
            // should modify this in the future to only pull the notes for the work order
            this.GetByIdForEdit();
            this.alertify.message('New note added');
          } else {
            this.alertify.error('There was an error adding this note. Please try again');
          }
        });
      }
    });
  }

  // confirm closing the work order
  confirmCloseDialog(): boolean {
    const closeOrder = false;
    const dialogRef = this.dialog.open(WorkOrderConfirmCloseComponent, {
      width: '350px',
      height: '350px',
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log(result);
    });

    return closeOrder;
  }

  /* TODO: find a way to get rid of duplicate code
  for this method */
  updateWorkOrder(editForm: FormGroup): void {
    const user = this.userService.getLoginUser();
    let canClose: boolean;
    const updatedWorkOrder: WorkOrderUpdateDto = {
      id: this.workOrderId,
      description: this.workOrder.description,
      workOrderType: { id: this.selectedTypeId},
      status: { id: this.selectedStatusId}
    };

    // if work order status is set to 'Closed' display a modal
    // to confirm the user wants to close the work order
    if (updatedWorkOrder.status.id === 5) {
      const dialogRef = this.dialog.open(WorkOrderConfirmCloseComponent, {
        width: '350px',
        height: '350px',
        disableClose: true
      });

      dialogRef.afterClosed().subscribe(result => {
        canClose = result;
        if (result) {
          this.workOrderService.updateWorkOrder(updatedWorkOrder, user.id).subscribe((httpsStatusCode: number) => {
            if (!httpsStatusCode) {
              this.alertify.message('Work order is closed');
              editForm.reset();
              this.GetByIdForEdit();
            } else {
              this.alertify.error('There was an error adding this note. Please try again');
            }
          });
        }
      });
      // process the workOrderService -> update normally
    } else {
      this.workOrderService.updateWorkOrder(updatedWorkOrder, user.id).subscribe((httpsStatusCode: number) => {
        if (!httpsStatusCode) {
          this.alertify.success('Changes to work order were successful');
          editForm.reset();
          this.GetByIdForEdit();
        } else {
          this.alertify.error('There was an error adding this note. Please try again');
        }
      });
    }
  }

  assignToLoggedInUser() {
    this.user = this.userService.getLoginUser();
    if (this.user.id === this.workOrder.assignedTo.id) {
      this.alertify.error('This Work Order is already assigned to you');
    } else {
      const updateAssignedWorkOrder: WorkOrderAssignDto = {
        id: this.workOrder.id,
        assignToId: this.user.id
      };
      this.workOrderService.assignToLoggedInUser(updateAssignedWorkOrder).subscribe((httpsStatuscode: number) => {
        if (!httpsStatuscode) {
          this.GetByIdForEdit();
          this.alertify.message('This Work Order is now assigned to you');
        } else {
          this.alertify.error('There was an error adding this note. Please try again');
        }
      });
    }
  }

  triggerResize() {
    // Wait for changes to be applied, then trigger textarea resize.
    this.ngZone.onStable.pipe(take(1))
        .subscribe(() => this.autosize.resizeToFitContent(true));
  }

}
