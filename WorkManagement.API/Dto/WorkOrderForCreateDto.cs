using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkManagement.API.Dto
{
    public class WorkOrderForCreateDto
    {
        public int WorkOrderTypeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AssignedTo { get; set; }
        public int RequestFor { get; set; }
        public int SubmittedBy { get; set; }
    }
}
