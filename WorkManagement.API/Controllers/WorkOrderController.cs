using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using WorkManagement.API.Repository;

namespace WorkManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WorkOrderController: ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly IWorkOrderRepository _workOrderRepository;
        
        public WorkOrderController(IConfiguration config, IMapper mapper, IWorkOrderRepository workOrderRepository)
        {
            _config = config;
            _mapper = mapper;
            _workOrderRepository = workOrderRepository;
        }

        [HttpGet("GetAllByUser/{userId}")]
        public IActionResult GetAllByUser(int userId)
        {
            var result = _workOrderRepository.GetAllByUser(userId);
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _workOrderRepository.GetById(id);
            return Ok(result);
        }
    }
}
