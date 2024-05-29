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
        private readonly Lazy<IMailService> _mailService;
        private readonly Lazy<ISuggestionsService> _suggestionsService;
        public ServiceManager(IRepositoryManager repositoryManager)
        {
            _userService = new Lazy<IUserService>(() => new UserManager(repositoryManager));
            _mailService = new Lazy<IMailService>(() => new MailManager(repositoryManager));
            _suggestionsService = new Lazy<ISuggestionsService>(() => new SuggestionsManager());
        }

        public IUserService UserService => _userService.Value;
        public IMailService MailService => _mailService.Value;

        public ISuggestionsService SuggestionsService => _suggestionsService.Value;
    }
}
