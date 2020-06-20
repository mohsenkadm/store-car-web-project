using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Serialization;
using store_car_web_project.Classes;
using store_car_web_project.Controllers;
using store_car_web_project.Models.Entity.Security;
using store_car_web_project.Models.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace store_car_web_project.Models.Services
{
    public class UserInterface :MasterController, IUserInterface 
    {   private readonly IUserServices _userServices;
       
        public UserInterface(PblogsContext context, IMemoryCache Cache,IUserServices userServices )
        {
            _context = context;
         _Cache = Cache;
        _userServices = userServices;
        }
        public async Task<object> Login(string username, string password)
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
                return Json(new { success = true, data = user, msg = "تم تسجيل االدخول بنجاح" });
            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " UserInterface => Login");
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء عملية تسجيل الدخول" });
            }
        }

        public async Task<object> Register(string username, string password, string email)
        {
            try
            {
                Users check = await _userServices.CheckUserinfo(username, email);
                if (check != null)
                return Json(new { success = false, msg = "عذرا اسم المتسخدم او البريد الالكتروني  محجوز" });
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
                // MailService mail = new MailService();
                // mail.SendMail(email, "car store", "Confirm Account Code : " + users.Code);

                await _context.Users.AddAsync(users);
                await _context.SaveChangesAsync();
                return  Json(new { success = true, msg = "تم انشاء الحساب  بنجاح  يرجى متابعه البريد الالكتروني لتاكيده" });
            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " UserInterface => Register");
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء عملية انشاء الحساب" });
            }
        }

        private object Json(object p)
        {
            return p;
        }

        public async Task<object> ConfirmAccount(string code, string username, string Email)
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

        public async Task<object> Logout()
        {
            try
            {
                Users users = await _userServices.getbyid(_UserId);
                if (users == null)
                    return Json(new { success = false, msg = "معلومات التاكيد غير صحيحه" });

                users.Isonline = false;
                users.Lastlogout = DateTime.UtcNow;

               _context.Entry(users).State = EntityState.Modified;
                await _context.SaveChangesAsync();
               UserManger = null;
                return Json(new { success = true });

            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " UserInterface => Logout");
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء عملية تسجيل الخروج" });
            }
        }
    }

}
