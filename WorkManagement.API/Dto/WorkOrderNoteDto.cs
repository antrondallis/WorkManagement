using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkManagement.API.Dto
{
    public class WorkOrderNoteDto
    {
        public int Id { get; set; }
        public string Note { get; set; }
        public DateTime CreateDate { get; set; }
        public UserFullNameDto CreatedBy { get; set; }

        public WorkOrderNoteDto()
        {
            CreatedBy = new UserFullNameDto();
        }
    }
}
