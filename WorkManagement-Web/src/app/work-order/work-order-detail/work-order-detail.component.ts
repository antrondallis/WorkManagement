import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { WorkOrder } from 'src/app/_models/workorder';
import { WorkOrderService } from 'src/app/_services/work-order.service';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-work-order-detail',
  templateUrl: './work-order-detail.component.html',
  styleUrls: ['./work-order-detail.component.css']
})
export class WorkOrderDetailComponent implements OnInit {

  workOrderId: number;
  workOrder: WorkOrder = null;
  constructor(private route: ActivatedRoute, private workOrderService: WorkOrderService,
              private alertify: AlertifyService) { }

  ngOnInit() {
    this.route.params.subscribe(p => {
      this.workOrderId = p.id;
    });

    this.GetById();
    // console.log('work order id: ' + this.workOrderId);
  }

  GetById() {
    this.workOrderService.getById(this.workOrderId).subscribe((workOrder: WorkOrder) => {
      this.workOrder = workOrder;
      console.log(workOrder);
    }, error => {
      this.alertify.error(error);
    });
  }

}
