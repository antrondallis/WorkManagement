import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Tenant } from '../_models/tenant';
import { Observable } from 'rxjs';
import { Formatter } from '../helpers/formatter';


@Injectable({
  providedIn: 'root'
})
export class TenantService {
  baseUrl = environment.apiUrl + 'tenant/';
  constructor(private http: HttpClient, private formatter: Formatter) { }

  lookUpTenant(tenant: Tenant): Observable<Tenant[]> {
    return this.http.get<Tenant[]>(this.baseUrl + 'lookuptenant?' + this.formatter.formatLookupTenantQuery(tenant));
  }

}
