using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WorkManagement.API.DAL;
using WorkManagement.API.Dto;
using WorkManagement.API.Models;

namespace WorkManagement.API.Repository
{
    public class WorkOrderRepository: IWorkOrderRepository
    {
        private WorkOrderDL _workOrderDL;
        private readonly IConfiguration _config;

        public WorkOrderRepository(IConfiguration config)
        {
            _config = config;
            _workOrderDL = new WorkOrderDL(_config);
        }

        public List<WorkOrderForListDto> GetAllByUser(int userId)
        {
            return _workOrderDL.GetAllByUser(userId);
        }

        public async Task<WorkOrderForDetailDto> GetById(int id)
        {
            var result = await _workOrderDL.GetById(id);
            result.WorkOrderNotes = await _workOrderDL.GetNotesForWorkOrder(id);
            return result;
        }

        public async Task<WorkOrderForEditDto> GetWorkOrderForEdit(int id)
        {
            var result = await _workOrderDL.GetWorkOrderForEdit(id);
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
            var result = await _workOrderDL.UpdateWorkOrder(workOrder);

            var log = new WorkOrderStatusLog()
            {
                WorkOrderId = workOrder.Id,
                UserId = userId,
                StatusId = workOrder.Status.Id
            };

            var logResult = await _workOrderDL.LogStatusChange(log);
            if (logResult == HttpStatusCode.BadRequest)
                return HttpStatusCode.BadRequest;

            return result;
        }
    }
}
