import { WorkOrderType } from './workordertype';
import { User } from './user';
import { Tenant } from './tenant';
import { WorkOrderStatusCode } from './workorderstatuscode';
import { WorkOrderNote } from './workordernote';

export interface WorkOrderForEdit {
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