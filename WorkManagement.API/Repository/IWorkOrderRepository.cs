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
        List<WorkOrderForListDto> GetAllByUser(int userId);
        Task<WorkOrderForDetailDto> GetById(int id);
        Task<DashboardCountDto> GetCountsForDashboard(int userId);
        Task<List<WorkOrderType>> GetWorkOrderTypes();
        Task<List<WorkOrderStatusCode>> GetWorkOrderStatusCodes();
        Task<WorkOrderForEditDto> GetWorkOrderForEdit(int id);
        Task<HttpStatusCode> AddWorkOrderNote(WorkOrderNote workOrderNote);
        Task<HttpStatusCode> UpdateWorkOrder(int userId, WorkOrder workOrder);

    }
}
