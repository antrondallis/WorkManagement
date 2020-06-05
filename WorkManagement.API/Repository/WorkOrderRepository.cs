using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WorkManagement.API.DAL;
using WorkManagement.API.Data;
using WorkManagement.API.Dto;
using WorkManagement.API.Models;

namespace WorkManagement.API.Repository
{
    public class WorkOrderRepository : IWorkOrderRepository
    {
        private WorkOrderDL _workOrderDL;
        private readonly IConfiguration _config;
        private readonly DataContext _context;

        public WorkOrderRepository(IConfiguration config, DataContext context)
        {
            _config = config;
            _context = context;
            _workOrderDL = new WorkOrderDL(_config);
        }

        public async Task<WorkOrder> CreateWorkOrder(WorkOrderForCreateDto workOrderForCreate)
        {
            var result = await _workOrderDL.CreateWorkOrder(workOrderForCreate);
            var createdWorkOrder = await _workOrderDL.GetById(result.Id);

            return createdWorkOrder;
        }
        public async Task<List<WorkOrder>> GetAllOpen()
        {
            var result = await _workOrderDL.GetAllOpen();
            return result;
        }

        public async Task<List<WorkOrder>> GetAllByUser(int userId)
        {
            var result = await _workOrderDL.GetAllByUser(userId);
            return result;
        }
        public async Task<List<WorkOrder>> GetAllOpenByUser(int userId)
        {
            var result = await _workOrderDL.GetAllOpenByUser(userId);
            return result;
        }

        public async Task<WorkOrder> GetById(int id)
        {
            var result = await _workOrderDL.GetById(id);
            result.WorkOrderNotes = await _workOrderDL.GetNotesForWorkOrder(id);
            return result;
        }

        public async Task<WorkOrder> GetWorkOrderForEdit(int id)
        {
            var result = await _workOrderDL.GetById(id);
            result.WorkOrderNotes = await _workOrderDL.GetNotesForWorkOrder(id);
            return result;
        }

        public async Task<DashboardCountDto> GetCountsForDashboard(int userId)
        {
            var result = await _workOrderDL.GetCountsForDashboard(userId);
            return result;
        }

        public async Task<List<WorkOrderType>> GetWorkOrderTypes()
        {
            var result = await _workOrderDL.GetWorkOrderTypes();
            return result;
        }

        public async Task<List<WorkOrderStatusCode>> GetWorkOrderStatusCodes()
        {
            var result = await _workOrderDL.GetWorkOrderStatusCodes();
            return result;
        }

        public async Task<HttpStatusCode> AddWorkOrderNote(WorkOrderNote workOrderNote)
        {
            var result = await _workOrderDL.AddWorkOrderNote(workOrderNote);
            return result;
        }

        public async Task<HttpStatusCode> UpdateWorkOrder(int userId, WorkOrder workOrder)
        {
            var statusCode = await _workOrderDL.GetWorkOrderStatusCode(workOrder.Id);
            var result = await _workOrderDL.UpdateWorkOrder(workOrder);

            // only log status change if the status actually changes 
            // e.g. :the work order can be updated without changes to the status


            if (workOrder.Status.Id != statusCode)
            {
                var log = new WorkOrderStatusLog()
                {
                    WorkOrderId = workOrder.Id,
                    UserId = userId,
                    StatusId = workOrder.Status.Id
                };

                var logResult = await _workOrderDL.LogStatusChange(log);

                if (logResult == HttpStatusCode.BadRequest)
                    return HttpStatusCode.BadRequest;
            }

            return result;
        }

        public async Task<HttpStatusCode> AssignToLoggedInUser(WorkOrderAssignDto workOrder)
        {
            var result = await _workOrderDL.AssignToLoggedInUser(workOrder);
            return result;
        }
    }
}
