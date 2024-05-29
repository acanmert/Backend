using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suggestions.Entities.Models
{
    public class FileUploadViewModel
    {
        public List<string> FileNames { get; set; }
        public List<string> FieldList { get; set; }
        public string ThisFileName { get; set; }
    }
}
