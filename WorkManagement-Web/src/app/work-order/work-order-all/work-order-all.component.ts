import { Component, OnInit } from '@angular/core';
import { WorkOrder } from 'src/app/_models/workorder';
import { WorkOrderService } from 'src/app/_services/work-order.service';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-work-order-all',
  templateUrl: './work-order-all.component.html',
  styleUrls: ['./work-order-all.component.css']
})
export class WorkOrderAllComponent implements OnInit {
  workOrders: WorkOrder[];

  constructor(private workOrderService: WorkOrderService, private alertify: AlertifyService) { }

  ngOnInit() {
    this.workOrderService.getAllOpen().subscribe(
      data => {
        this.workOrders = data;
      }
    );
  }

}
