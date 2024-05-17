using Microsoft.AspNetCore.Mvc;
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
            User user = new User()
            {
                Name = Name,
                SurName = SurName,
                Email = Email,
                Password = Password,
                PasswordConfirmed = PasswordConfirmed
            };

            if (!_service.CreateUser(user))
            {
                ModelState.AddModelError("Email", "This email is already in use.");
                return View();
            }
            User? createdUser = _service.GetUser(user.Email);
            return RedirectToAction("İndex", "Home", createdUser);
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

            return View();
        }
        public IActionResult LogOut()
        {
            return View();
        }
    }
}
