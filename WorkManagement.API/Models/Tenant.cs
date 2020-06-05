using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkManagement.API.Models
{
    public class Tenant
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ApartmentNo { get; set; }
        public bool IsCurrentResident { get; set; }
    }
}
