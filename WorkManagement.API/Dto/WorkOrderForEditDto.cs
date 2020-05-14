using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkManagement.API.Models;

namespace WorkManagement.API.Dto
{
    public class WorkOrderForEditDto
    {
        public int Id { get; set; }
        public DateTime RequestDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public WorkOrderType WorkOrderType { get; set; }
        public UserFullNameDto AssignedTo { get; set; }
        public TenantForWorkOrderDto RequestFor { get; set; }
        public UserFullNameDto SubmittedBy { get; set; }
        public WorkOrderStatusCode Status { get; set; }
        public List<WorkOrderNoteDto> WorkOrderNotes { get; set; }

        public WorkOrderForEditDto()
        {
            WorkOrderType = new WorkOrderType();
            AssignedTo = new UserFullNameDto();
            RequestFor = new TenantForWorkOrderDto();
            SubmittedBy = new UserFullNameDto();
            Status = new WorkOrderStatusCode();
            WorkOrderNotes = new List<WorkOrderNoteDto>();
        }
    }
}
