import { Component, OnInit, Inject } from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { WorkOrderEditComponent } from '../work-order-edit/work-order-edit.component';
import { WorkOrderNote } from 'src/app/_models/workordernote';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-work-order-notes-add-dialog',
  templateUrl: './work-order-notes-add-dialog.component.html',
  styleUrls: ['./work-order-notes-add-dialog.component.css']
})
export class WorkOrderNotesAddDialogComponent implements OnInit {
  addNoteForm: FormGroup;
  workOrderNote: WorkOrderNote;
  constructor( public dialogRef: MatDialogRef<WorkOrderEditComponent>,
               @Inject(MAT_DIALOG_DATA) public data: WorkOrderNote) { }

  ngOnInit() { }

  onNoClick(): void {
    this.dialogRef.close();
  }

  addNote() {
    console.log('add this ' + this.data.note);
  }

}
