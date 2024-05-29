using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Suggestions.Business.Abstract;
using Suggestions.Entities.Models;

namespace Backend.Controllers
{
    public class RegisterController : Controller
    {

        protected readonly IServiceManager _serviceManager;

        public RegisterController(IServiceManager serviceManager)
        {

            _serviceManager = serviceManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(string Name, string SurName, string Email, string Password, string PasswordConfirmed)
        {
            if (_serviceManager.MailService.CheckEmail(Email))
            {
                ModelState.AddModelError("Email", "Bu Email Kullanılıyor");
                return View();
            }

            if (Password != PasswordConfirmed)
            {
                ModelState.AddModelError("Password", "Girilen Şifreler Aynı Değil");
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
