using System;
using System.Collections.Generic;
using System.Linq;
using BlogApi.Models;
using System.Threading.Tasks;

namespace BlogApi.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        Task<RegisterUserModel> Create(RegisterUserModel user);
        User GetById(int id);
    }
}