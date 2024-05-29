using Microsoft.AspNetCore.Mvc;
using Suggestions.Business.Abstract;

namespace Backend.Controllers
{
    public class AccountController : Controller
    {
        private IServiceManager _serviceManager;

        public AccountController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public IActionResult Index()
        {
            
            return View();
        }
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LogIn(string Email, string Password)
        {
            if (!_serviceManager.UserService.CheckUser(Email, Password))
            {
                ModelState.AddModelError("Email", "Yanlıs E Mail veya Şifre Yeniden Deneyiniz");
                return View();
            }
            TempData["Email"] = Email;
            TempData.Keep("Email");

            return RedirectToAction("Index", "Suggestions");
        }
        public IActionResult LogOut()
        {
            return View();
        }
    }
}
