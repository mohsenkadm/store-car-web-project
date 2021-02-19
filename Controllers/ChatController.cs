using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using store_car_web_project.Classes;
using store_car_web_project.Models;
using store_car_web_project.Models.Entity.Security;
using store_car_web_project.Models.IServices;
using X.PagedList;

namespace store_car_web_project.Controllers
{
    public class ChatController : MasterController
    {
        private readonly IChatServices _chatServices;
        public ChatController(PblogsContext context,IChatServices chatServices, IMemoryCache cache, IHubContext<Signalr> hubContext)
        {
            _context = context;
            _chatServices = chatServices;
            _Cache = cache;
            _hubContext = hubContext;
        }
        [Route("Chat/Chat/{user_id}")]
        public IActionResult Chat(int user_id)
        {
            _userId2 = user_id;
            user_idManger = user_id;
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
                if (User.Identity.IsAuthenticated)
                {
                    Messag message = await _chatServices.GetCountMessage(UserManger.Id);
                    return Json(new { success = true, data = message });
                }
                else
                {
                    return Json(new { success = false});
                }
            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " ChatController => GetCountMessage");
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء جلب البيانات" });
            }
        }
        [HttpGet]
        public async Task<JsonResult> GetMessagechat()
        {
            try
            {
                List<Messag> message = await _chatServices.GetMessagechat(UserManger.Id,user_idManger);
                return Json(new { success = true, data = message });
            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " ChatController => GetMessagechat");
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
                await log.WriteAsync(ex, " ChatController => SetCountMessage");
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء جلب البيانات" });
            }
        }
        [HttpPost]
        public async Task<JsonResult> SendMessage(string message)
        {
            try
            {
                Messag messag = new Messag
                {
                    seen = false,
                    Message_date = DateTime.UtcNow,
                    Message_txt = message,
                    User_reciver_id =user_idManger,
                    User_sender_id = UserManger.Id,
                };
               
              await  _context.messags.AddAsync(messag);
                await _context.SaveChangesAsync();
                _chatServices.SetCountMessage(UserManger.Id);
                await _hubContext.Clients.All.SendAsync("displaymessage", "");
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " ChatController => SendMessage");
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء ارسال الرسالة" });
            }
        }
        [HttpGet]
        public async Task<JsonResult> GetMessage()
        {
            try
            {
                List<Messag> message = await _chatServices.GetMessage(UserManger.Id);
                return Json(new { success = true, data = message });
            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " ChatController => GetMessage");
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء جلب البيانات" });
            }
        }
        
    }
}
