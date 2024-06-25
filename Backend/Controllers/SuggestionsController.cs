using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Suggestions.Business.Abstract;
using Suggestions.Entities.Models;
using System.Text;

namespace Backend.Controllers
{
    [Authorize]
    public class SuggestionsController : Controller
    {
        private static readonly HttpClient _client = new HttpClient();
        private IServiceManager _serviceManager;
        private static int _queryCount = 0;

        public SuggestionsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public IActionResult Index()
        {
            return View(_serviceManager.SuggestionsService.GetFile());
        }
        [HttpGet]
        public IActionResult DataUpload()
        {

            return View(_serviceManager.SuggestionsService.GetFile());
        }
        [HttpPost]
        public IActionResult DataUpload(IFormFile formFile)
        {

            var dataUpload = _serviceManager.SuggestionsService.DataUpload(formFile);
            return View(dataUpload);

        }
        public IActionResult Suggestions(string fileName)
        {
            string email = TempData["Email"].ToString();
            TempData.Keep("Email");

            var user = _serviceManager.UserService.GetUser(email);
            FileUploadViewModel file = new FileUploadViewModel();

            file.FieldList = _serviceManager.SuggestionsService.Header(fileName);
            file.FileNames = _serviceManager.SuggestionsService.GetFile();
            file.ThisFileName = fileName;
            return View(file);
        }
        public async Task<IActionResult> ProcessSuggestions(string fileName, List<string> selectedFeatures, string p_pk, string p_name, string p_type)
        {

            //string token = TempData["Token"].ToString();
            //_client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var recommendations = await _serviceManager.SuggestionsService.GetRecommendations(fileName, selectedFeatures, p_pk, p_name, p_type, _queryCount);

            TempData.Keep("Email");

            return View(recommendations);
        }
        public IActionResult LogUser()
        {
            return View();
        }

        public IActionResult Download(List<string> data)
        {
            var csvContent = string.Join(Environment.NewLine, data);

            var fileName = "suggestions.csv";
            Response.Headers.Add("Content-Disposition", "attachment; filename=" + fileName);
            Response.ContentType = "text/csv";


            return File(Encoding.UTF8.GetBytes(csvContent), "text/csv", fileName);

        }
    }
}
