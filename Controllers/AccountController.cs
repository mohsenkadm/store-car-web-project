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
    public class AccountController : MasterController
    {
        private readonly IUserServices _userServices;
       public AccountController(PblogsContext context, IMemoryCache Cache , IUserServices userServices)
        {
            _context = context;
            _Cache = Cache;
            _userServices = userServices;
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
        public async Task<JsonResult> Login(string username,string password)
        {
            try
            {
                //daaper
                Users user = await _userServices.Login(username, password);

                //_context.Users.Where(x=>x.UserName==username && x.Password==password).AsNoTracking().FirstOrDefault();
                if (user == null)
                { return Json(new { secces = false, msg = "عذرا اسم المستخدم او كلمة المرور خطا" }); }
                 
                user.Isonline = true;
                user.Lastlogin = DateTime.UtcNow;
                user.Token = new JsonWebToken().GenerateToken(user.Id,username);
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                _UserId = user.Id;
                UserManger = user;
                return Json(new { secces = true, data=user, msg = "تم تسجيل االدخول بنجاح" });
            }
            catch(Exception ex)
            {
                await log.WriteAsync(ex, " AccountController => Login");
                return Json(new { secces = false, msg = "عذرا حدث خطا اثناء عملية تسجيل الدخول" });
            }

        }
        [HttpPost]
        public async Task<JsonResult> Register(string username, string password,string email)
        {
            try
            {
                Users check = await _userServices.CheckUserinfo(username, email);
                if(check!=null)
                    return Json(new { secces = false, msg = "عذرا اسم المتسخدم او البريد الالكتروني  محجوز" });
                Random random = new Random();
                Users users = new Users
                {
                    UserName = username,
                    Email = email,
                    Password = password,
                    Isonline = false,
                    IsActive = true,
                    IsDeletet = false,
                    InsertDate = DateTime.UtcNow,
                    IsConfirm = false,
                    Code = random.Next(100000, 999999).ToString()
                };
                MailService mail = new MailService();
               mail.SendMail(email, "car store","Confirm Account Code : "+users.Code);

               await _context.Users.AddAsync(users);
              await  _context.SaveChangesAsync();
                return Json(new { secces = true, msg = "تم انشاء الحساب  بنجاح  يرجى متابعه البريد الالكتروني لتاكيده" });
            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " AccountController => Register");
                return Json(new { secces = false, msg = "عذرا حدث خطا اثناء عملية انشاء الحساب" });
            }


        }
        [HttpPost]
        public async Task<JsonResult> ConfirmAccount(string code, string username, string Email)
        {
            try
            {
                Users users = await _userServices.checkConfirmAccount(code,username, Email);
                if (users == null)
                    return Json(new { secces = false, msg = "معلومات التاكيد غير صحيحه" });

                users.IsConfirm = true;

                _context.Entry(users).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Json(new { secces = true, msg = "تم تاكيد الحساب بنجاح" });

            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " AccountController => ConnfirmAccount");
                return Json(new { secces = false, msg = "عذرا حدث خطا اثناء عملية انشاء الحساب" });
            }
        }
        [HttpGet]
        [Authorize]
        public async Task<JsonResult> Logout(string code, string username, string Email)
        {
            try
            {
                Users users = await _userServices.getbyid(_UserId);
                if (users == null)
                    return Json(new { secces = false, msg = "معلومات التاكيد غير صحيحه" });

                users.Isonline = false;
                users.Lastlogout = DateTime.UtcNow;

                _context.Entry(users).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                UserManger = null;
                return Json(new { secces = true });

            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " AccountController => Logout");
                return Json(new { secces = false, msg = "عذرا حدث خطا اثناء عملية تسجيل الخروج" });
            }
        }
    }

}