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

  editForm: FormGroup;

  constructor(private route: ActivatedRoute, private workOrderService: WorkOrderService,
              private alertify: AlertifyService, private ngZone: NgZone, private dialog: MatDialog,
              private userService: UserService) { }

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
            this.alertify.success('New note added');
          } else {
            this.alertify.error('There was an error adding this note. Please try again');
          }
        });
      }
    });
  }

  updateWorkOrder(editForm: FormGroup) {
    editForm.reset();
    this.alertify.success('Changes to work order were successful');
    this.GetByIdForEdit();
  }

  triggerResize() {
    // Wait for changes to be applied, then trigger textarea resize.
    this.ngZone.onStable.pipe(take(1))
        .subscribe(() => this.autosize.resizeToFitContent(true));
  }

}
