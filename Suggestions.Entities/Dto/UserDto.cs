using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suggestions.Entities.Dto
{
    public class UserDto
    {

        [StringLength(40)]
        public string Name { get; set; }
        [StringLength(40)]
        public string SurName { get; set; }
        [StringLength(40)]
        public string Email { get; set; }
        [StringLength(40)]
        public string Password { get; set; }
        public string PasswordConfirmed { get; set; }
    }
}
