using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ReflectionIT.Mvc.Paging;
using store_car_web_project.Models;
using store_car_web_project.Models.IServices;
using X.PagedList;
namespace store_car_web_project.Controllers
{
    public class UsercontrolController : MasterController
    {

        private readonly IUserServices _userServices;
        private readonly IPostsServices _postsServices;
        public UsercontrolController(PblogsContext context , IMemoryCache Cache , IUserServices userServices,IPostsServices postsServices)
        {
            _Cache = Cache;
            _context = context;
            _postsServices = postsServices;

            _userServices = userServices;
        
        }
        [HttpGet]
        public IActionResult usersview(string searching, int? page)
        {
            var pageNumber = page ?? 1; // if no page is specified, default to the first page (1)
            var list = _userServices.getusers();
            if (searching != null)
            {
                list = list.Where(x => x.UserName.StartsWith(searching) || searching == null).ToList();
            }
            return View(list.ToPagedList(pageNumber, 6));
        }
        [HttpGet]
        public IActionResult postsview(string searching, int? page)
        {
            var pageNumber = page ?? 1; // if no page is specified, default to the first page (1)
            var list = _postsServices.getpostall();
            if (searching != null)
            {
                list = list.Where(x => x.title.StartsWith(searching) || searching == null).ToList();
            }
            return View(list.ToPagedList(pageNumber, 6));
        }
        public async Task<IActionResult> DeletePost(int? id)
        {
            try
            {
                var t = _context.Posts.Find(id);
                _context.Posts.Remove(t);
                await _context.SaveChangesAsync();
                return Json(new { success = true, msg = "تم  حذف المنشور بنجاح" });
            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " PostsInterface => DeletePosts");
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء عملية حذف المنشور" });
            }
           
        }
        public async Task<IActionResult> DeleteUser(int? id)
        {
            try
            {
                var t = _context.Users.Find(id);
                _context.Users.Remove(t);
                await _context.SaveChangesAsync();
                return Json(new { success = true, msg = "تم  حذف مستخدم بنجاح" });
            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " PostsInterface => DeletePosts");
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء عملية حذف مستخدم" });
            }
        }
    }
}
