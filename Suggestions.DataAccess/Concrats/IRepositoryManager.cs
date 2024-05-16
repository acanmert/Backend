using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suggestions.DataAccess.Concrats
{
    public interface IRepositoryManager
    {
        IUserRepository User { get; }
        void Save();
    }
}
