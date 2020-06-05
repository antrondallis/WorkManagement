import { Tenant } from '../tenant';

export interface TenantResultResponse {
    tenants: Tenant[];
    response: string;
    selectedTenant: Tenant;
}
