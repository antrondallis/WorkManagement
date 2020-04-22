using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkManagement.API.Models;

namespace WorkManagement.API.Dto
{
    public class WorkOrderForDetailDto
    {
        public int Id { get; set; }
        public DateTime RequestDate { get; set; }
        public string WorkOrderType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AssignedTo { get; set; }
        public string ApartmentNo { get; set; }
        public string RequestFor { get; set; }
        public string SubmittedBy { get; set; }
        public string Status { get; set; }
        public List<WorkOrderNote> WorkOrderNotes { get; set; }
    }
}
