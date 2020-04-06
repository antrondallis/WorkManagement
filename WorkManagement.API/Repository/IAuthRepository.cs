using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkManagement.API.Dto;
using WorkManagement.API.Models;

namespace WorkManagement.API.Repository
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<string> GenerateUsername(UserForRegisterDto user);
        Task<User> Login(string username, string password);
    }
}
