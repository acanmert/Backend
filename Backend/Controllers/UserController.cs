using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }      
        public IActionResult LogIn()
        {
            return View();
        }
    }
}
