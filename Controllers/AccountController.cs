using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using store_car_web_project.Classes;
using store_car_web_project.Models;
using store_car_web_project.Models.Entity.Security;
using store_car_web_project.Models.IServices;

namespace store_car_web_project.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserInterface _userInterface;
        
        public AccountController(IUserInterface userInterface)
        {
        
            _userInterface = userInterface;
        }
        public IActionResult SignIn()
        {
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Login(string username,string password)
        {
            return Json(_userInterface.Login(username, password).Result);
        }
        [HttpPost]
        public JsonResult Register(Users users)
        {
            Random random = new Random();
            users.Isonline = false;
            users.IsActive = true;
            users.IsDeletet = false;
            users.InsertDate = DateTime.UtcNow;
            users.IsConfirm = false;
            users.Code = random.Next(100000, 999999).ToString();
            

            return Json(_userInterface.Register(users).Result);
        }
        [HttpPost]
        public JsonResult ConfirmAccount(string code, string username, string Email)
        {
            return Json(_userInterface.ConfirmAccount(code, username, Email).Result);
        }
        [HttpGet]
        [Authorize]
        public JsonResult Logout()
        {
            return Json(_userInterface.Logout());
          
        }
    }

}