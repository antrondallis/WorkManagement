import { User } from './user';

export class WorkOrderNote {
    id: number;
    workOrderId: number;
    note: string;
    createDate: Date;
    createdBy: User;
}
