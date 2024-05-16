using Suggestions.DataAccess.Concrats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suggestions.DataAccess.EfCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly Lazy<IUserRepository> _UserRepository;

        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _UserRepository = new Lazy<IUserRepository>(() => new UserRepository(_context));

        }
        public IUserRepository User=> _UserRepository.Value;

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
