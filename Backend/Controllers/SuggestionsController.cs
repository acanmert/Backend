using Microsoft.AspNetCore.Mvc;
using Suggestions.Business.Abstract;
using Suggestions.Entities.Models;

namespace Backend.Controllers
{
    public class SuggestionsController : Controller
    {
        private static readonly HttpClient _client = new HttpClient();
        private IServiceManager _serviceManager;

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
        public  IActionResult DataUpload(IFormFile formFile)
        {
            var dataUpload =  _serviceManager.SuggestionsService.DataUpload(formFile);
            return View(dataUpload);

        }
        public IActionResult Suggestions(string fileName)
        {
            FileUploadViewModel file = new FileUploadViewModel();

            file.FieldList = _serviceManager.SuggestionsService.Header(fileName);
            file.FileNames = _serviceManager.SuggestionsService.GetFile();
            file.ThisFileName = fileName;
            return View(file);
        }
        public async Task<IActionResult> ProcessSuggestions(string fileName, List<string> selectedFeatures, string p_pk, string p_name, string p_type)
        {


            var recommendations = await _serviceManager.SuggestionsService.GetRecommendations(fileName, selectedFeatures, p_pk, p_name, p_type);
            return View(recommendations);
        }
        public IActionResult LogUser()
        {
            return View();
        }

    }
}
