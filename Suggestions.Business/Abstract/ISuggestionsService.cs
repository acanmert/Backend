using Microsoft.AspNetCore.Http;
using Suggestions.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suggestions.Business.Abstract
{
    public interface ISuggestionsService
    {
        List<string> GetFile(string email);
        List<string> DataUpload(IFormFile formFile,string Email);
        Task<List<string>> GetRecommendations(string fileName, List<string> benzerlikName, string p_primaryKey, string getProductName, string p_type,int requestCount, string Email);
        List<string> Header(string fileName,string email);
        FileUploadViewModel Suggestions(string fileName,string email);
    }
}
