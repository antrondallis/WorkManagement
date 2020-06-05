using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WorkManagement.API.Models;
using WorkManagement.API.Repository;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorkManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TenantController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ITenantRepository _tenantRepository;

        public TenantController(IConfiguration config, ITenantRepository tenantRepository)
        {
            _config = config;
            _tenantRepository = tenantRepository;
        }
        
        [HttpGet("LookupTenant")]
        public async Task<IActionResult> LookupTenant([FromQuery]string apartmentNo, [FromQuery] string firstName, [FromQuery] string lastName)
        {
            var tenant = new Tenant()
            {
                ApartmentNo = apartmentNo,
                FirstName = firstName,
                LastName = lastName
            };

            var result = await _tenantRepository.LookupTenant(tenant);
            return Ok(result);
        }
    }
}
