using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        
        public AccountController(PblogsContext context,IUserServices userServices, IMemoryCache cache)
        {
            _context = context;
            _userServices = userServices;
            _Cache = cache;
        }
        [AllowAnonymous]
        [Route("Account/login1")]
        public IActionResult SignIn()
        {
           /// this.ViewData["ReturnUrl"] = returnUrl;
            return this.View();
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
                Users user = await _userServices.Login_chek(username, password);
                //_context.Users.Where(x=>x.UserName==username && x.Password==password).AsNoTracking().FirstOrDefault();
                if (user == null)
                { return Json(new { success = false, msg = "عذرا اسم المستخدم او كلمة المرور خطا" }); }
                user.Isonline = true;
                user.Lastlogin = DateTime.UtcNow;
                user.Token = new JsonWebToken().GenerateToken(user.Id, username);
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                _UserId = user.Id;
                UserManger = user;
                return Json(new { success = true, data = user.Token, msg = "تم تسجيل االدخول بنجاح" });
            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " UserInterface => Login");
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء عملية تسجيل الدخول" });
            }
        
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("Account/Register")]
        public async Task<JsonResult> Register(Users users)
        {
        
            try
            {
                Users check = await _userServices.CheckUserinfo(users.UserName, users.Email);
                if (check != null)
                    return Json(new { success = false, msg = "عذرا اسم المتسخدم او البريد الالكتروني  محجوز" });
                Random random = new Random();
                users.Isonline = false;
                users.IsActive = true;
                users.IsDeletet = false;
                users.InsertDate = DateTime.UtcNow;
                users.IsConfirm = false;
                users.Code = random.Next(100000, 999999).ToString();
                MailService mail = new MailService();
                mail.SendMail(users.Email, "car store", "Confirm Account Code : " + users.Code);

                await _context.Users.AddAsync(users);
                await _context.SaveChangesAsync();
                return Json(new { success = true, msg = "تم انشاء الحساب  بنجاح  يرجى متابعه البريد الالكتروني لتاكيده" });
            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " UserInterface => Register");
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء عملية انشاء الحساب" });
            }
         
        }
        [HttpPost]
        public async Task<JsonResult> ConfirmAccount(string code, string username, string Email)
        {
            try
            {
                Users users = await _userServices.checkConfirmAccount(code, username, Email);
                if (users == null)
                    return Json(new { success = false, msg = "معلومات التاكيد غير صحيحه" });

                users.IsConfirm = true;

                _context.Entry(users).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Json(new { success = true, msg = "تم تاكيد الحساب بنجاح" });

            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " UserInterface => ConnfirmAccount");
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء عملية انشاء الحساب" });
            }
        }
        [HttpGet]
        [Authorize]
        public async Task<JsonResult> Logout()
        {
            try
            {
                Users users = await _userServices.getbyid(UserManger.Id);
                if (users == null)
                    return Json(new { success = false, msg = "عمليه تسجيل الدخول لم تكتمل" });

                users.Isonline = false;
                users.Lastlogout = DateTime.UtcNow;

                _context.Entry(users).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                UserManger = null;
                return Json(new { success = true, msg = "تم تسجيل الخروج بنجاح" });

            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " UserInterface => Logout");
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء عملية تسجيل الخروج" });
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<JsonResult> GetUserInfo(int i)
        {
            if (i == 1)
            {
                try
                {
                    List<Users> users = await _userServices.GetUserInfo(1);

                    if (users == null)
                        return Json(new { success = false, msg = "عذرا حدث خطا اثناء عملية جلب البيانات" });

                    return Json(new { success = true, data = users });

                }
                catch (Exception ex)
                {
                    await log.WriteAsync(ex, " PostsInterface => GetUserInfo");
                    return Json(new { success = false, msg = "عذرا حدث خطا اثناء جلب البيانات" });
                }
            }
            else if (i == 2)
            {
                try
                {
                    List<Users> users = await _userServices.GetUserInfo(2);
                    if (users == null)
                        return Json(new { success = false, msg = "عذرا حدث خطا اثناء عملية جلب البيانات" });

                    return Json(new { success = true, data = users });
                }
                catch (Exception ex)
                {
                    await log.WriteAsync(ex, " PostsInterface => GetUserInfo");
                    return Json(new { success = false, msg = "عذرا حدث خطا اثناء جلب البيانات" });
                }
            }
            else return Json(null);
        }   
    }

}