using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkManagement.API.Models
{
    public class WorkOrderStatusLog
    {
        public int Id { get; set; }
        public int WorkOrderId { get; set; }
        public int UserId { get; set; }
        public int StatusId { get; set; }
        public DateTime ActivityDate { get; set; }

    }
}
