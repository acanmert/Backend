using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Suggestions.Business.Abstract;
using Suggestions.Entities.Models;

namespace Backend.Controllers
{
    [Authorize]

    public class RegisterController : Controller
    {
        protected readonly ILoggerService _loggerService;
        protected readonly IServiceManager _serviceManager;

        public RegisterController(IServiceManager serviceManager, ILoggerService loggerService)
        {

            _serviceManager = serviceManager;
            _loggerService = loggerService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Suggestions");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register(string Name, string SurName, string Email, string Password, string PasswordConfirmed)
        {
            if (_serviceManager.MailService.CheckEmail(Email))
            {
                string message = "Bu email adresi kullanılıyor";
                _loggerService.LogError(message);
                ModelState.AddModelError("Email", message);
                return View();
            }

            if (Password != PasswordConfirmed)
            {
                ModelState.AddModelError("Password", "Girilen Şifreler Aynı Değil");
                return View();
            }
            if (Name.Length>20 && SurName.Length>20)
            {
                ModelState.AddModelError("Name", "İsim Çok Uzun");
                return View();
            }

            Random rnd = new Random();
            int code = rnd.Next(1000, 9999);

            User user = new User()
            {
                Name = Name,
                SurName = SurName,
                Email = Email,
                Password = Password,
                PasswordConfirmed = PasswordConfirmed,
                ConfirmationCode = code

            };

            TempData["Code"] = user.ConfirmationCode;

            return RedirectToAction("EmailVerification", user);

        }
        [AllowAnonymous]
        public IActionResult EmailVerification(User user)
        {
            if (TempData["Code"] != null)
            {
                _serviceManager.MailService.EmailSendCode(user, "Register");
                TempData.Keep("Code");

                return View(user);
            }
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult EmailVerification(User user, string Code)
        {

            if (_serviceManager.MailService.EmailConfirmation(user, int.Parse(Code)))
            {
                TempData.Keep("Code");
                user.EmailCheck = "true";
                _serviceManager.UserService.CreateUser(user);
                _serviceManager.MailService.EmailSendCode(user, "Login");

                return RedirectToAction("LogIn","Account"); // Yönlendirme yapabilirsiniz
            }
            TempData.Keep("Code");
            ModelState.AddModelError("ConfirmationCode", "Hatalı kod girdiniz");

            return View();
        }

    }
}
