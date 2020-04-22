import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { User } from '../_models/user';
import { UserService } from '../_services/user.service';
import { WorkOrderService } from '../_services/work-order.service';
import { WorkOrder } from '../_models/workorder';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  workOrders: WorkOrder[];
  user: User;
  title = 'Dashboard';

  constructor(private route: ActivatedRoute, private userService: UserService,
              private workOrderService: WorkOrderService, private alertify: AlertifyService) { }

  ngOnInit() {
    this.user = this.userService.getLoginUser();
    this.workOrderService.getAllByUser(this.user.id).subscribe(
      data => {
        this.workOrders = data;
        console.log(this.workOrders);
      }
    );
    // this.GetAllByUser(this.user.id);
  }

  GetAllByUser(userId) {
    this.workOrderService.getAllByUser(userId).subscribe((workOrders: WorkOrder[]) => {
      this.workOrders = workOrders;
      console.log(this.workOrders.length);
    }, error => {
      this.alertify.error(error);
    });
  }

}
