using Suggestions.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suggestions.Business.Abstract
{
    public interface IUserService
    {
        List<User> GetAllUser();
        User? GetUser(string Email);
        bool CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
        bool CheckUser(string Email,string password);
    }
}
