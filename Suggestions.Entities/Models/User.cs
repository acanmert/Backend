using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suggestions.Entities.Models
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(40)]
        public string Name { get; set; }
        [StringLength(40)]
        public string SurName { get; set; }
        [StringLength(40)]
        public string Email { get; set; }
        [StringLength(40)]
        public string Password { get; set; }
        public string? EmailCheck { get; set; }
        [StringLength(40)]
        public int? RequestCount { get; set; }
        [StringLength(40)]
        public string PasswordConfirmed { get; set; }
        public int? ConfirmationCode { get; set; }

    }
}
