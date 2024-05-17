using Suggestions.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suggestions.DataAccess.Concrats
{
    public interface IUserRepository:IRepositoryBase<User>
    {
        public bool DoesEmailExist(string email);
        User? GetUser(string email);
        void DeleteUser(User user);
        void UpdateUser(User user);
        void CreateUser(User user);

        List<User> GetAlUsers();


    }
}
