using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WorkManagement.API.Dto;
using WorkManagement.API.Models;

namespace WorkManagement.API.Repository
{
    public interface IWorkOrderRepository
    {
        Task<List<WorkOrder>> GetAllOpen();
        Task<List<WorkOrder>> GetAllByUser(int userId);
        Task<List<WorkOrder>> GetAllOpenByUser(int userId);
        Task<WorkOrder> GetById(int id);
        Task<DashboardCountDto> GetCountsForDashboard(int userId);
        Task<List<WorkOrderType>> GetWorkOrderTypes();
        Task<List<WorkOrderStatusCode>> GetWorkOrderStatusCodes();
        Task<WorkOrder> GetWorkOrderForEdit(int id);
        Task<HttpStatusCode> AddWorkOrderNote(WorkOrderNote workOrderNote);
        Task<HttpStatusCode> UpdateWorkOrder(int userId, WorkOrder workOrder);
        Task<HttpStatusCode> AssignToLoggedInUser(WorkOrderAssignDto workOrder);
        Task<WorkOrder> CreateWorkOrder(WorkOrderForCreateDto workOrderForCreate);
    }
}
