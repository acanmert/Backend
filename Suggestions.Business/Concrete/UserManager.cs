using Microsoft.EntityFrameworkCore;
using Suggestions.Business.Abstract;
using Suggestions.DataAccess.Concrats;
using Suggestions.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suggestions.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IRepositoryManager _manager;

        public UserManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public bool CheckUser(string Email, string password)
        {
            var user = GetUser(Email);
            if (user == null)
            {
                return false;
            }
            if (user.Password == password)
            {
                return true;
            }
            return false;
        }

        public bool CreateUser(User user)
        {

            if (!_manager.User.DoesEmailExist(user.Email))
            {
                _manager.User.Create(user);
                _manager.Save();
                return true;
            }
            return false;
        }


        public void DeleteUser(User user)
        {
            if (!_manager.User.DoesEmailExist(user.Email))
            {
                _manager.User.Delete(user);
            }

        }

        public List<User> GetAllUser()
        {
            return _manager.User.GetAlUsers();
        }

        public User? GetUser(string Email)
        {
            var Liste = _manager.User.GetAlUsers();
            return Liste.FirstOrDefault(u => u.Email == Email);
        }

        public void UpdateUser(User user)
        {
            if (!_manager.User.DoesEmailExist(user.Email))
            {
                _manager.User.Update(user);
            }
        }
    }
}
