using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Suggestions.Business.Abstract;
using Suggestions.Entities.Models;

namespace Backend.Controllers
{
    public class RegisterController : Controller
    {
        protected readonly IUserService _service;

        public RegisterController(IUserService service)
        {
            _service = service;
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
            if (_service.CheckEmail(Email))
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
                _service.EmailSendCode(user, "Register");
                TempData.Keep("Code");

                return View(user);
            }
            return View();
        }
        [HttpPost]
        public IActionResult EmailVerification(User user, string Code)
        {
            if (TempData["Code"].ToString() == Code)
            {
                TempData.Keep("Code");
                user.EmailCheck = "true";
                _service.CreateUser(user);
                _service.EmailSendCode(user, "Login");

                return RedirectToAction("LogIn"); // Yönlendirme yapabilirsiniz
            }
            TempData.Keep("Code");
            ModelState.AddModelError("ConfirmationCode", "Hatalı kod girdiniz");

            return View();
        }
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LogIn(string Email, string Password)
        {
            if (!_service.CheckUser(Email, Password))
            {
                ModelState.AddModelError("Email", "Yanlıs E Mail veya Şifre Yeniden Deneyiniz");
                return View();
            }
            TempData["Email"] = Email;
            TempData.Keep("Email");

            return RedirectToAction("UserSettings", "User");
        }
        public IActionResult LogOut()
        {
            return View();
        }
    }
}
