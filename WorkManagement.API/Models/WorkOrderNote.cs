using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkManagement.API.Models
{
    public class WorkOrderNote
    {
        public int Id { get; set; }
        public int WorkOrderId { get; set; }
        public string Note { get; set; }
        public DateTime CreateDate { get; set; }
        public User Createdby { get; set; }
    }
}
