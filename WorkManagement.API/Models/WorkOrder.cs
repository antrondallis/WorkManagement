using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkManagement.API.Models
{
    public class WorkOrder
    {
        public int Id { get; set; }
        public DateTime RequestDate { get; set; }
        public WorkOrderType WorkOrderType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public User AssignedTo { get; set; }
        public Tenant RequestFor { get; set; }
        public User SubmittedBy { get; set; }
        public WorkOrderStatusCode Status { get; set; }
    }
}
