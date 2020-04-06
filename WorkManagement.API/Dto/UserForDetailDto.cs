using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkManagement.API.Models;

namespace WorkManagement.API.Dto
{
    public class UserForDetailDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public string Username { get; set; }
        public AccountTypeCode AccountType { get; set; }
        public DateTime LastActive { get; set; }
    }
}
