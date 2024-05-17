using Suggestions.DataAccess.Concrats;
using Suggestions.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suggestions.DataAccess.EfCore
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext context) : base(context)
        {
        }
        public bool DoesEmailExist(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public void CreateUser(User user) => Create(user);

        public void DeleteUser(User user) => Delete(user);

        public List<User> GetAlUsers() => FindAll().ToList();

        public User? GetUser(string Email)
        {

            return _context.Set<User>().FirstOrDefault(u => u.Email == Email);
        }
        public void UpdateUser(User user) => Update(user);
    }
}
