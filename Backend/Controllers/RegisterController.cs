using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Suggestions.Business.Abstract;
using Suggestions.Entities.Models;

namespace Backend.Controllers
{
    public class RegisterController : Controller
    {
        protected readonly IUserService _userService;
        protected readonly IMailService _mailService;

        public RegisterController(IUserService service, IMailService mailService)
        {
            _userService = service;
            _mailService = mailService;
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
            if (_mailService.CheckEmail(Email))
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
                _mailService.EmailSendCode(user, "Register");
                TempData.Keep("Code");

                return View(user);
            }
            return View();
        }
        [HttpPost]
        public IActionResult EmailVerification(User user, string Code)
        {
            
            if (_mailService.EmailConfirmation(user,int.Parse(Code)))
            {
                TempData.Keep("Code");
                user.EmailCheck = "true";
                _userService.CreateUser(user);
                _mailService.EmailSendCode(user, "Login");

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
            if (!_userService.CheckUser(Email, Password))
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
