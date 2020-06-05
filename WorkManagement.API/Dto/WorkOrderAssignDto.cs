using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkManagement.API.Dto
{
    public class WorkOrderAssignDto
    {
        public int Id { get; set; }
        public int AssignToId { get; set; }
    }
}
