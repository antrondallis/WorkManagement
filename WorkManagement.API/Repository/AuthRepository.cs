using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkManagement.API.Data;
using WorkManagement.API.Dto;
using WorkManagement.API.Models;

namespace WorkManagement.API.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.UserName == username);

            if (user == null)
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }
        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<string> GenerateUsername(UserForRegisterDto user)
        {
            int i = 0;
            var username = $"{user.FirstName.Substring(0, 1).ToLower()}{user.LastName.ToLower()}";
            //check if username already exists
            while(await _context.User.AnyAsync(x => x.UserName == username))
            {
                i += 1;
                username = $"{username}{i.ToString()}";
            }

            return username;
        }

        private bool VerifyPasswordHash(string password, byte[]passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            }

            return true;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
