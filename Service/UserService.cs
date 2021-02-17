using System;
using System.Collections.Generic;
using System.Linq;
using BlogApi.Models;
using System.Threading.Tasks;

namespace BlogApi.Services
{
    public class UserService : IUserService
    {
        private readonly BlogDbContext _context;

        public UserService(BlogDbContext context)
        {
            _context = context;
        }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.Users.SingleOrDefault(x => x.Username == username);

            if (user == null)
                return null;

            if (!user.ComparePassword(password))
                return null;

            // authentication successful
            return user;
        }
        public async Task<RegisterUserModel> Create(RegisterUserModel user)
        {
            if (string.IsNullOrWhiteSpace(user.Password))
                throw new Exception("Password is required");

            if (_context.Users.Any(x => x.Username == user.Username))
                throw new Exception("Username \"" + user.Username + "\" is already taken");
            User userToAdd = new User()
            {
                UserFullName = user.UserFullName,
                Email = user.Email,
                Username = user.Username
            };
            userToAdd.SetPassword(user.Password);
            await _context.Users.AddAsync(userToAdd);
            await _context.SaveChangesAsync();
            return user;
        }
        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

    }
}