using Suggestions.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suggestions.Business.Abstract
{
    public interface IMailService
    {
        void EmailSendCode(User user, string type);
        bool EmailConfirmation(User user, int? UserCode);
        bool CheckEmail(string Email);
    }
}
