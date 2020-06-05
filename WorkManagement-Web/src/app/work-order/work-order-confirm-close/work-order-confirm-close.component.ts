import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { WorkOrderEditComponent } from '../work-order-edit/work-order-edit.component';

@Component({
  selector: 'app-work-order-confirm-close',
  templateUrl: './work-order-confirm-close.component.html',
  styleUrls: ['./work-order-confirm-close.component.css']
})
export class WorkOrderConfirmCloseComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<WorkOrderEditComponent>,
              @Inject(MAT_DIALOG_DATA) public data: boolean) { }

  ngOnInit() {
  }

  onNoClick(): void {
    this.data = false;
    // console.log(this.data);
    this.dialogRef.close();
  }

  canClose(): void {
    this.data = true;
    // console.log(this.data);
    this.dialogRef.close();
  }

}
