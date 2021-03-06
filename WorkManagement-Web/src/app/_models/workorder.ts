import { WorkOrderNote } from './workordernote';
import { WorkOrderType } from './workordertype';
import { WorkOrderStatusCode } from './workorderstatuscode';
import { User } from './user';
import { Tenant } from './tenant';

export interface WorkOrder {
    id: number;
    requestDate: Date;
    title: string;
    description: string;
    workOrderType: WorkOrderType;
    assignedTo: User;
    requestFor: Tenant;
    submittedBy: User;
    status: WorkOrderStatusCode;
    workOrderNotes: WorkOrderNote[];
}
