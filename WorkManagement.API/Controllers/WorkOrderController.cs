using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Threading.Tasks;
using WorkManagement.API.Models;
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

        [HttpGet("GetWorkOrderForEdit/{id}")]
        public async Task<IActionResult> GetWorkOrderForEdit(int id)
        {
            var result = await _workOrderRepository.GetWorkOrderForEdit(id);
            return Ok(result);
        }

        [HttpGet("GetCountsForDashboard/{userId}")]
        public async Task<IActionResult> GetCountsForDashboard(int userId)
        {
            var result = await _workOrderRepository.GetCountsForDashboard(userId);
            return Ok(result);
        }

        [HttpGet("GetWorkOrderTypes")]
        public async Task<IActionResult> GetWorkOrderTypes()
        {
            var result = await _workOrderRepository.GetWorkOrderTypes();
            return Ok(result);
        }

        [HttpGet("GetWorkOrderStatusCodes")]
        public async Task<IActionResult> GetWorkOrderStatusCodes()
        {
            var result = await _workOrderRepository.GetWorkOrderStatusCodes();
            return Ok(result);
        }

        [HttpPost("AddWorkOrderNote")]
        public async Task<IActionResult> AddWorkOrderNote(WorkOrderNote workOrderNote)
        {
            var result = await _workOrderRepository.AddWorkOrderNote(workOrderNote);
            if (result == HttpStatusCode.Created)
                return StatusCode(201);

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPut("UpdateWorkOrder/{userId}")]
        public async Task<IActionResult> UpdateWorkOrder(int userId, WorkOrder workOrder)
        {
            if (userId == 0 || workOrder.Id == 0)
                return BadRequest();
            var result = await _workOrderRepository.UpdateWorkOrder(userId, workOrder);
            if (result == HttpStatusCode.NoContent)
                return StatusCode(StatusCodes.Status204NoContent);

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
