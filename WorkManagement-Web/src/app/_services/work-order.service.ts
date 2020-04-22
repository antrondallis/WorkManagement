import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { WorkOrder } from '../_models/workorder';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class WorkOrderService {
  baseUrl = environment.apiUrl + 'workorder/';
  constructor(private http: HttpClient) { }

  getAllByUser(userId: number): Observable<WorkOrder[]> {
    return this.http.get<WorkOrder[]>(this.baseUrl + 'GetAllByUser/' + userId);
  }
}
