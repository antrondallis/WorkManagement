using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkManagement.API.Dto;
using WorkManagement.API.Models;

namespace WorkManagement.API.Repository
{
    public interface IWorkOrderRepository
    {
        List<WorkOrderForListDto> GetAllByUser(int userId);
        Task<WorkOrderForDetailDto> GetById(int id);
    }
}
