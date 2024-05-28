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
        void UpdateUser(User user,string email);
        void DeleteUser(User user);
        bool CheckUser(string Email,string password);
        void EmailSendCode(User user,string type);
        bool EmailConfirmation(User user,int? UserCode);
        bool CheckEmail(string Email);
    }
}
