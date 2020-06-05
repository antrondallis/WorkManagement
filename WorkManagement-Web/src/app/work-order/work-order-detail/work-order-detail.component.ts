import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { WorkOrder } from 'src/app/_models/workorder';
import { WorkOrderService } from 'src/app/_services/work-order.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { Formatter } from 'src/app/helpers/formatter';

@Component({
  selector: 'app-work-order-detail',
  templateUrl: './work-order-detail.component.html',
  styleUrls: ['./work-order-detail.component.css']
})
export class WorkOrderDetailComponent implements OnInit {

  workOrderId: number;
  workOrder: WorkOrder = null;
  constructor(private route: ActivatedRoute, private workOrderService: WorkOrderService,
              private alertify: AlertifyService, private formatter: Formatter) { }

  ngOnInit() {
    this.route.params.subscribe(p => {
      this.workOrderId = p.id;
    });

    this.GetById();
    console.log(this.workOrder);
  }

  GetById() {
    this.workOrderService.getById(this.workOrderId).subscribe((workOrder: WorkOrder) => {
      this.workOrder = workOrder;
      this.formatter.formatAssignName(this.workOrder.assignedTo);
    }, error => {
      this.alertify.error(error);
    });
  }

}
