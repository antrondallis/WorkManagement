using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkManagement.API.DataLayer;
using WorkManagement.API.Models;

namespace WorkManagement.API.Repository
{
    public class TenantRepository: ITenantRepository
    {
        private TenantDL _tenantDL;
        private readonly IConfiguration _config;

        public TenantRepository(IConfiguration config)
        {
            _config = config;
            _tenantDL = new TenantDL(_config);
        }
        public async Task<List<Tenant>> LookupTenant(Tenant tenant)
        {
            var result = await _tenantDL.LookupTenant(tenant);
            return result;
        }
    }
}
