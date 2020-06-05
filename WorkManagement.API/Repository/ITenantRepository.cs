using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkManagement.API.Models;

namespace WorkManagement.API.Repository
{
    public interface ITenantRepository
    {
        Task<List<Tenant>> LookupTenant(Tenant tenant);
    }
}
