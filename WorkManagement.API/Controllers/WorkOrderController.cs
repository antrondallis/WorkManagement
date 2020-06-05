using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Threading.Tasks;
using WorkManagement.API.Dto;
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

        [HttpPost("CreateWorkOrder")]
        public async Task<IActionResult> CreateWorkOrder(WorkOrderForCreateDto workOrderForCreate)
        {
            var result = await _workOrderRepository.CreateWorkOrder(workOrderForCreate);
            return CreatedAtRoute("GetById", new { id = result.Id }, result);
        }

        [HttpGet("GetAllOpen")]
        public async Task<IActionResult> GetAllOpen()
        {
            var result = await _workOrderRepository.GetAllOpen();
            return Ok(result);
        }

        [HttpGet("GetAllByUser/{userId}")]
        public async Task<IActionResult> GetAllByUser(int userId)
        {
            var result = await _workOrderRepository.GetAllByUser(userId);
            return Ok(result);
        }

        [HttpGet("GetAllOpenByUser/{userId}")]
        public async Task<IActionResult> GetAllOpenByUser(int userId)
        {
            var result = await _workOrderRepository.GetAllOpenByUser(userId);
            return Ok(result);
        }

        [HttpGet("GetById/{id}", Name = "GetById")]
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

        [HttpPut("AssignToLoggedInUser")]
        public async Task<IActionResult> AssignToLoggedInUser(WorkOrderAssignDto workOrder)
        {
            var result = await _workOrderRepository.AssignToLoggedInUser(workOrder);
            if (result == HttpStatusCode.NoContent)
                return StatusCode(StatusCodes.Status204NoContent);

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
