import { Injectable } from '@angular/core';
import { User } from '../_models/user';
import { Tenant } from '../_models/tenant';

@Injectable({
    providedIn: 'root'
})
export class Formatter {
    constructor() {}

    formatAssignName(user: User) {
        if (user.id === 0) {
            user.firstName = 'Unassigned';
        }
        return user;
    }

    formatTenantSearchParam(tenant: Tenant) {
        if (tenant.firstName === '') {
            tenant.firstName = null;
        }
        if (tenant.lastName === '') {
            tenant.lastName = null;
        }
        if (tenant.apartmentNo === '') {
            tenant.apartmentNo = null;
        }

        return tenant;
    }

    formatLookupTenantQuery(tenant: Tenant) {
        return `apartmentNo=${tenant.apartmentNo}&firstname=${tenant.firstName}&lastname=${tenant.lastName}`;
    }
}
