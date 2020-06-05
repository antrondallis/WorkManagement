import { StatusUpdateDto } from './statusupdatedto';
import { TypeUpdateDto } from './typeupdatedto';

export interface WorkOrderUpdateDto {
    id: number;
    description: string;
    status: StatusUpdateDto;
    workOrderType: TypeUpdateDto;
}
