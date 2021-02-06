using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using store_car_web_project.Models;
using store_car_web_project.Models.Entity.Security;
using store_car_web_project.Models.IServices;
using X.PagedList;

namespace store_car_web_project.Controllers
{
    public class ChatController : MasterController
    {
        private readonly IChatServices _chatServices;
        public ChatController(PblogsContext context,IChatServices chatServices, IMemoryCache cache)
        {
            _context = context;
            _chatServices = chatServices;
            _Cache = cache;
        }
        public IActionResult Chat()
        {
            return View();
        }
        public IActionResult ListChat()
        {
            //string nameuser, int? page
            // var pageNumber = page ?? 1; // if no page is specified, default to the first page (1)
            // return View(_userInterface.GetUser(nameuser).ToPagedList(pageNumber, 10));
            return View();
        }
        [HttpGet]
        public async Task<JsonResult> GetCountMessage()
        {
            try
            {
                Messag message = await _chatServices.GetCountMessage(UserManger.Id);
                return Json(new { success = true, data = message });
            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " ChatInterface => GetCountMessage");
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء جلب البيانات" });
            }
        }
        [HttpPost]
        public async Task<JsonResult> SetCountMessage()
        {
            try
            {
                _chatServices.SetCountMessage(UserManger.Id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " ChatInterface => SetCountMessage");
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء جلب البيانات" });
            }
        }
        [HttpGet]
        public async Task<JsonResult> GetMessage()
        {
            try
            {
                Messag message = await _chatServices.GetMessage(UserManger.Id);
                return Json(new { success = true, data = message });
            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " ChatInterface => GetMessage");
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء جلب البيانات" });
            }
        }
        
    }
}
