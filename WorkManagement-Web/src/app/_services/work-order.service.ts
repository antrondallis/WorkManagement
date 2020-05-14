import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { WorkOrder } from '../_models/workorder';
import { Observable } from 'rxjs';
import { DashboardCount } from '../_models/dashboardcount';
import { WorkOrderType } from '../_models/workordertype';
import { map } from 'rxjs/operators';
import { WorkOrderStatusCode } from '../_models/workorderstatuscode';
import { WorkOrderNote } from '../_models/workordernote';

const httpOptions = {
  headers: new HttpHeaders({
    'Cache-Control':  'no-cache, no-store, must-revalidate, post-check=0, pre-check=0',
        Pragma: 'no-cache',
        Expires: '0'
  })
}

@Injectable({
  providedIn: 'root'
})
export class WorkOrderService {
  baseUrl = environment.apiUrl + 'workorder/';
  constructor(private http: HttpClient) { }

  getAllByUser(userId: number): Observable<WorkOrder[]> {
    return this.http.get<WorkOrder[]>(this.baseUrl + 'GetAllByUser/' + userId);
  }

  getById(workOrderId: number): Observable<WorkOrder> {
    return this.http.get<WorkOrder>(this.baseUrl + 'GetById/' + workOrderId);
  }

  getByIdForEdit(workOrderId: number): Observable<WorkOrder> {
    return this.http.get<WorkOrder>(this.baseUrl + 'GetWorkOrderForEdit/' + workOrderId);
  }

  getCountsForDashboard(userId: number): Observable<DashboardCount> {
    return this.http.get<DashboardCount>(this.baseUrl + 'GetCountsForDashboard/' + userId);
  }

  getWorkOrderTypes(): Observable<WorkOrderType[]> {
    return this.http.get<WorkOrderType[]>(this.baseUrl + 'GetWorkOrderTypes');
  }

  getWorkOrderStatusCodes(): Observable<WorkOrderStatusCode[]> {
    return this.http.get<WorkOrderStatusCode[]>(this.baseUrl + 'GetWorkOrderStatusCodes');
  }

  addWorkOrderNote(workOrderNote: WorkOrderNote): Observable<number> {
    return this.http.post<number>(this.baseUrl + 'AddWorkOrderNote', workOrderNote);
  }
}
