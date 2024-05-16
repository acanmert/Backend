using Suggestions.Business.Abstract;
using Suggestions.DataAccess.Concrats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suggestions.Business.Concrete
{
    public class ServiceManager:IServiceManager
    {
        private readonly Lazy<IUserService> _userService;
        public ServiceManager(IRepositoryManager repositoryManager)
        {
            _userService = new Lazy<IUserService>(() => new UserManager(repositoryManager));
        }
    }
}
