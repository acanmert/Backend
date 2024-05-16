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

        public void CreateUser(User user)
        {

           if(!_manager.User.DoesEmailExist(user.Email))
            {
                _manager.User.Create(user);
                _manager.Save();
            }
        


        }
    

        public void DeleteUser(User user)
        {
            if (_manager.User.GetUser(user.Email) is not null)
            {
                _manager.User.Delete(user);
            }
           
        }

        public List<User> GetAllUser()
        {
            return _manager.User.GetAlUsers().ToList();
        }

        public User? GetUser(string Email)
        {
            return _manager.User.GetUser(Email);
        }

        public void UpdateUser(User user)
        {
            if (_manager.User.GetUser(user.Email) is not null)
            {
                _manager.User.Update(user);
            }
        }
    }
}
