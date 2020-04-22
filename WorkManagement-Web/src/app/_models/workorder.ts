export interface WorkOrder {
    id: number;
    requestDate: Date;
    workOrderType: string;
    title: string;
    assignedTo: string;
    requestFor: string;
    apartmentNo: string;
    status: string;
}