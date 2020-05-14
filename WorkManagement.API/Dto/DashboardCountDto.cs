using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkManagement.API.Dto
{
    public class DashboardCountDto
    {
        public int TotalWorkOrders { get; set; }
        public int AssignedWorkOrders { get; set; }
        public int AgeTwoWeeks { get; set; }
        public int Unassigned { get; set; }
    }
}
