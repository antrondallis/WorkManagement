﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkManagement.API.Models;

namespace WorkManagement.API.Dto
{
    public class WorkOrderForListDto
    {
        public int Id { get; set; }
        public DateTime RequestDate { get; set; }
        public string WorkOrderType { get; set; }
        public string Title { get; set; }
        public string AssignedTo { get; set; }
        public string RequestFor { get; set; }
        public string ApartmentNo{ get; set; }
        public string Status { get; set; }
    }
}
