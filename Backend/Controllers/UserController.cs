using AutoMapper;
using Backend.AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Suggestions.Business.Abstract;
using Suggestions.Entities.Dto;
using Suggestions.Entities.Models;

namespace Backend.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;
        public UserController(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult UserSettings()
        {
            string Email = TempData["Email"].ToString();
            TempData.Keep("Email");

            //var user=_service.GetUser(Email);
            //var userDto = new UserDtoForUpdate("","","","","","");
            //_mapper.Map(userDto,user );

            return View();
        }
        [HttpPost]
        public IActionResult UserSettings(UserDtoForUpdate userDto)
        {

            string email = TempData["Email"].ToString();
            User entity = _service.GetUser(email);
            _mapper.Map(userDto, entity);
            var deneme = 2;
            _service.UpdateUser(entity);
            

            return RedirectToAction("Index","Home");
        }

    }
}
