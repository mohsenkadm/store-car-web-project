using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Formatters;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using store_car_web_project.Models;
using store_car_web_project.Models.Entity.Security;
using store_car_web_project.Models.IServices;

namespace store_car_web_project.Controllers
{
    public class BlogsController : MasterController
    {
        private readonly IPostsServices _postsServices;
        private readonly IWebHostEnvironment _hostEnvironment;
        private string FilePath;
        public BlogsController(PblogsContext context,IMemoryCache Cache, IPostsServices postsServices,IWebHostEnvironment hostEnvironment)
        {

            _Cache = Cache;
            _context = context;
            _postsServices = postsServices;
            this._hostEnvironment = hostEnvironment;
        }
        public IActionResult Index(decimal type_id)
        {
            return View();
        }

        public IActionResult posts()
        {
            return View();
        }
        public IActionResult profile()
        {
            return View();
        }
        [HttpGet]
        public async Task<JsonResult> Posts_get(int type_id)
        {
            try
            {
                List<Posts> posts1 = await _postsServices.getpost(type_id);
                if (posts1 == null)
                    return Json(new { secces = false, msg = "عذرا حدث خطا اثناء عملية جلب البيانات" });

                return Json(new { secces = true, data = posts1 });
            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " BlogsController => Posts_get");
                return Json(new { secces = false, msg = "عذرا حدث خطا اثناء جلب البيانات" });
            }
        }
        [HttpGet]
        public async Task<JsonResult> likes_get(int post_id)
        {
            try
            {
                List<Posts> posts1 = await _postsServices.getlike(post_id);
                if (posts1 == null)
                    return Json(new { secces = false, msg = "عذرا حدث خطا اثناء عملية جلب البيانات" });

                return Json(new { secces = true, data = posts1 });
            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " BlogsController => likes_get");
                return Json(new { secces = false, msg = "عذرا حدث خطا اثناء جلب البيانات" });
            }
        }
        [HttpGet]
        public async Task<JsonResult> commend_get(int post_id)
        {
            try
            {
             List<Posts> posts1 = await _postsServices.getcommend(post_id);
                if (posts1 == null)
                    return Json(new { secces = false, msg = "عذرا حدث خطا اثناء عملية جلب البيانات" });

                return Json(new { secces = true, data = posts1 });
            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " BlogsController => commend_get");
                return Json(new { secces = false, msg = "عذرا حدث خطا اثناء جلب البيانات" });
            }
        }
        [HttpPost]
        public async Task Upload_FileAsync()
        {
           
            try
            {
                FilePath = "";
                long size = 0;var file = Request.Form.Files;
                var filename = ContentDispositionHeaderValue.Parse(file[0].ContentDisposition).FileName.Trim('"');
                 FilePath = _hostEnvironment.WebRootPath + $@"\images\imag_post\{ filename}";
                size += file[0].Length;
                using (FileStream fs = System.IO.File.Create(FilePath))
                {
                    file[0].CopyTo(fs);
                    fs.Flush();
                }
              
            }

            catch (Exception ex)
            {
                await log.WriteAsync(ex, " BlogsController => Upload_File");
            }
           
        }
        [HttpPost]
        public async Task<JsonResult> Posts_insert(string title, string commend, string address, int price, int phone, string image,string Company_id,int Model, int type_id)
        {
            try
            {
                Posts posts = new Posts
                {
                    title = title,
                    commend = commend,
                    address = address,
                    price = price,
                    phone = phone,
                    date = DateTime.UtcNow,
                    image = FilePath,
                    Company_id = Company_id,
                    Model = Model,
                    Type_id = type_id,
                    user_id = 1,
                    post_id2 = 0,
                    like = false,
                    count_like = 0,
                    count_comment = 0,
                };

                posts.image = _hostEnvironment.WebRootPath + $@"\images\imag_post"+image.Substring(image.LastIndexOf("\\"));
                await _context.Posts.AddAsync(posts);
                await _context.SaveChangesAsync();
                return Json(new { secces = true, msg = "تم انشاء المنشور  بنجاح  يرجى متابعه صفحتك لعرض المنشور" });
            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " BlogsController => Posts_insert");
                return Json(new { secces = false, msg = "عذرا حدث خطا اثناء عملية انشاء المنشور" });
            }


        }
        [HttpPost]
        public async Task<JsonResult> Commend_insert(int post_id, string commend)
        {
            try
            {
                Posts posts1 = await _postsServices.CheckAccount(post_id,UserManger.Id);
                if (posts1 == null)
                    return Json(new { secces = false, msg = "عذرا حدث خطا اثناء عملية التعليق" });

                Posts posts = new Posts
                {
                    title = "",
                    commend = commend,
                    address = "",
                    price = 0,
                    phone=0,
                    date = DateTime.UtcNow,
                    image = null,
                    Type_id = 0,
                    user_id = UserManger.Id,
                    post_id2 = posts1.post_id,
                    like = false,
                    count_like = 0,
                };

                await _context.Posts.AddAsync(posts);
                await _context.SaveChangesAsync();

                posts1.count_comment = posts1.count_comment + 1;
                _context.Entry(posts1).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Json(new { secces = true, msg = "تم التعليق  بنجاح  " });
            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " BlogsController => Commend_insert");
                return Json(new { secces = false, msg = "عذرا حدث خطا اثناء عملية التعليق" });
            }


        }
        [HttpPost]
        public async Task<JsonResult> like_insert(int post_id)
        {
            try
            {
                Posts posts1 = await _postsServices.CheckAccount(post_id, UserManger.Id);
                if (posts1 == null)
                    return Json(new { secces = false, msg = "عذرا حدث خطا اثناء عملية الاعجاب" });

                Posts posts = new Posts
                {
                    title = "",
                    commend = "",
                    address = "",
                    price = 0,
                    phone=0,
                    date = DateTime.UtcNow,
                    image = null,
                    Type_id = 0,
                    user_id = UserManger.Id,
                    post_id2 = posts1.post_id,
                    like = true,
                    count_like = 0,
                    count_comment = 0,
                };

                await _context.Posts.AddAsync(posts);
                await _context.SaveChangesAsync();

                posts1.count_like = posts1.count_like + 1;
                _context.Entry(posts1).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Json(new { secces = true, msg = "تم اعجاب بنجاح  " });
            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " BlogsController => like_insert");
                return Json(new { secces = false, msg = "عذرا حدث خطا اثناء عملية الاعجاب" });
            }


        }

        [HttpDelete]
        public async Task<JsonResult> delete_post(int post_id)
        {
            try
            {
                Posts posts1 = await _postsServices.CheckAccount(post_id, UserManger.Id);
                if (posts1 == null)
                    return Json(new { secces = false, msg = "عذرا حدث خطا اثناء عملية الحذف" });

                _context.Entry(posts1).State = EntityState.Deleted;
                await _context.SaveChangesAsync();

                return Json(new { secces = true, msg = "تم حذف المنشور بنجاح  " });
            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " BlogsController => delete_post");
                return Json(new { secces = false, msg = "عذرا حدث خطا اثناء عملية حذف المنشور" });
            }


        }

    }
}
