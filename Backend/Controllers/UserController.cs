using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Suggestions.Business.Abstract;
using Suggestions.Entities.Models;

namespace Backend.Controllers
{
    public class UserController : Controller
    {
        protected readonly IUserService _service;

        public UserController(IUserService service)
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


            if (_service.CheckEmail(user.Email))
            {
                ModelState.AddModelError("Email", "This email is already in use.");
                return View();
            }
            TempData["Email"] = user.ConfirmationCode;

            return RedirectToAction("EmailVerification",user);

        }
        public IActionResult EmailVerification(User user)
        {
            if (TempData["Email"] != null)
            {
                _service.EmailSendCode(user);
                TempData.Keep("Email");

                return View(user);
            }
            return View();
        }
        [HttpPost]
        public IActionResult EmailVerification(User user,string Code)
        {
            if (TempData["Email"].ToString() == Code)
            {
                TempData.Keep("Email");
                user.EmailCheck = "true";
                _service.CreateUser(user);
                return RedirectToAction("LogIn"); // Yönlendirme yapabilirsiniz
            }
            TempData.Keep("Email");
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

            return RedirectToAction("Index", "Home");
        }
        public IActionResult LogOut()
        {
            return View();
        }
    }
}
