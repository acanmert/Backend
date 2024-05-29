using Microsoft.AspNetCore.Mvc;
using Suggestions.Business.Abstract;
using Suggestions.Entities.Models;

namespace Backend.Controllers
{
    public class SuggestionsController : Controller
    {
        private static readonly HttpClient _client = new HttpClient();
        private ISuggestionsService _suggestionsService;

        public SuggestionsController(ISuggestionsService suggestionsService)
        {
            _suggestionsService = suggestionsService;
        }

        public IActionResult Index()
        {
            return View(_suggestionsService.GetFile());
        }
        [HttpGet]
        public IActionResult DataUpload()
        {

            return View(_suggestionsService.GetFile());
        }
        [HttpPost]
        public async Task<IActionResult> DataUpload(IFormFile formFile)
        {
            var dataUpload = _suggestionsService.DataUpload(formFile);
            return View(dataUpload);

        }
        public IActionResult Suggestions(string fileName)
        {
            FileUploadViewModel file = new FileUploadViewModel();

            file.FieldList = _suggestionsService.Header(fileName);
            file.FileNames = _suggestionsService.GetFile();
            file.ThisFileName = fileName;
            return View(file);
        }
        public async Task<IActionResult> ProcessSuggestions(string fileName, List<string> selectedFeatures, string p_pk, string p_name, string p_type)
        {


            var recommendations = await _suggestionsService.Get_recommendations(fileName, selectedFeatures, p_pk, p_name, p_type);
            return View(recommendations);
        }

    }
}
