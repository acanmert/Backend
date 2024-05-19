using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suggestions.Entities.Dto
{
    public record UserDtoForUpdate(string Name,string SurName,string Email,string Password,string PasswordConfirmed, string ConfirmationCode);

}
