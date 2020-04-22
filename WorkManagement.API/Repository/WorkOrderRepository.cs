using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return result;
        }
    }
}
