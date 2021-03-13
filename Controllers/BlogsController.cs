using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using store_car_web_project.Classes;
using store_car_web_project.Models;
using store_car_web_project.Models.Entity.Security;
using store_car_web_project.Models.IServices;

namespace store_car_web_project.Controllers
{
    [Authorize]
    public class BlogsController : MasterController
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IPostsServices _postsServices;
        private string FilePath;
        public BlogsController(PblogsContext context, IPostsServices postsServices,IMemoryCache cache, IHubContext<Signalr> hubContext, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _postsServices = postsServices;
            _Cache = cache;
            _hubContext = hubContext;
            this._hostEnvironment = hostEnvironment;
        }

        [Route("Blogs/Index/{type_id}")]
        public IActionResult Index(int type_id)
        {
               _TypeId =type_id;
            TypeManger = _TypeId;
            return View();
        }
        [AllowAnonymous]
        public IActionResult posts()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("login1", "Account");
            }
        }
        [Route("Blogs/Update_posts/{post_id}")]
        public IActionResult Update_posts(int post_id)
        {
            _post_id = post_id;
            postManger = _post_id;
            return View();
        }
        [AllowAnonymous]
        public IActionResult profile()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("login1", "Account");
            }
        }
       [AllowAnonymous]
        public IActionResult Advanced_Search()
        {
            _hubContext.Clients.All.SendAsync("displaynot", "");
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("login1", "Account");
            }
        }
        [Route("Blogs/profileUser/{user_id}")]
        public IActionResult profileUser(int user_id)
        {
            _userId2 = user_id;
            user_idManger = user_id;
            return View();
        }
        [Route("Blogs/Notifecation/{post_id}")]
        public IActionResult Notifecation(int post_id)
        {
            _post_id = post_id;
            postManger = _post_id;
            return View();
        }
        [HttpGet]
        public async Task<JsonResult> GetSearchPosts(string model, string company, string price, string type_id, string title)
        {
          
            try
            {
                if (model == null)
                    model = "";
                if (company == null)
                    company = "";
                if (price == null)
                    price = "0";
                if (type_id == null)
                    type_id = "0";
                if (title == null)
                    title = "";
                string where_code = "where  1=1 and post_id2=0 ";
                if (model.Trim().Length != 0 || company.Trim().Length != 0 || price.Trim().Length != 0 || type_id.Trim().Length != 0 || title.Trim().Length != 0)
                {
                    if (model.Trim().Length > 0)
                    { where_code += " and model = " + model; }
                    if (company.Trim().Length > 0)
                    { where_code += " and Company_id like ' " + company + "'"; }
                    if (price.Trim().Length > 0 && price != "0")
                    { where_code += "  and price like '" + price + "'"; }
                    if (type_id.Trim().Length > 0 && type_id != "0")
                    { where_code += "  and Type_id=" + type_id; }
                    if (title.Trim().Length > 0)
                    { where_code += "  and title like '" + title + "'"; }
                }
                else { where_code = " where 1=0"; }
                List<Posts> posts1 = await _postsServices.getsearchpost(where_code, UserManger.Id);
                if (posts1 == null)
                    return Json(new { success = false, msg = "عذرا حدث خطا اثناء عملية جلب البيانات" });

                return Json(new { success = true, data = posts1 });
            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " BlogsController => GetSearchPosts");
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء جلب البيانات" });
            }
        }
        [HttpGet]
        public async Task<JsonResult> GetNotifecationPosts()
        {
            try
            {
                string where_code = " where  1=1 and p.post_id2=0 ";

                if (postManger > 0)
                { where_code += "  and p.post_id =" + postManger; }

                else { where_code = " where 1=0"; }
                List<Posts> posts1 = await _postsServices.getsearchpost(where_code, UserManger.Id);
                if (posts1 == null)
                    return Json(new { success = false, msg = "عذرا حدث خطا اثناء عملية جلب البيانات" });

                return Json(new { success = true, data = posts1 });
            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " BlogsController => GetNotifecationPosts");
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء جلب البيانات" });
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetPosts()
        {
            try
            {
                List<Posts> posts1 = await _postsServices.getpost(TypeManger, UserManger.Id);
                if (posts1 == null)
                    return Json(new { success = false, msg = "عذرا حدث خطا اثناء عملية جلب البيانات" });

                return Json(new { success = true, data = posts1 });
            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " BlogsController => Posts_get");
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء جلب البيانات" });
            }
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("Blogs/Getimages/{post_id}")]
        public async Task<JsonResult> Getimages(int post_id)
        {
            try
            {
                List<Images> images = await _postsServices.getimages(post_id);
                if (images == null)
                    return Json(new { success = false, msg = "عذرا حدث خطا اثناء عملية جلب البيانات" });
                return Json(new { success = true, data = images });
            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " BlogsController => Posts_get");
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء جلب البيانات" });
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetPostsId()
        {
            try
            {
                Posts posts1 = await _postsServices.GetPostsId(postManger);
                if (posts1 == null)
                    return Json(new { success = false, msg = "عذرا حدث خطا اثناء عملية جلب البيانات" });

                return Json(new { success = true, data = posts1 });
            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " BlogsController => GetPostsId");
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء جلب البيانات" });
            }
        }
        [HttpGet]
        public async Task<JsonResult> GetPostsProfile(int i)
        {
            if (i == 1) {
                try
                {
                    List<Posts> posts1 = await _postsServices.getpostProfile(UserManger.Id, UserManger.Id);
                    if (posts1 == null)
                        return Json(new { success = false, msg = "عذرا حدث خطا اثناء عملية جلب البيانات" });

                    return Json(new { success = true, data = posts1 });
                }
                catch (Exception ex)
                {
                    await log.WriteAsync(ex, " BlogsController => GetPostsProfile");
                    return Json(new { success = false, msg = "عذرا حدث خطا اثناء جلب البيانات" });
                }
            }
           
        else if(i==2)
            {
                try
                {
                    List<Posts> posts1 = await _postsServices.getpostProfile(user_idManger, UserManger.Id);
                    if (posts1 == null)
                        return Json(new { success = false, msg = "عذرا حدث خطا اثناء عملية جلب البيانات" });

                    return Json(new { success = true, data = posts1 });
                }
                catch (Exception ex)
                {
                    await log.WriteAsync(ex, " BlogsController => GetPostsProfile");
                    return Json(new { success = false, msg = "عذرا حدث خطا اثناء جلب البيانات" });
                }
            }
              
            return Json(null);
        }
       
        [HttpGet]
        public async Task<JsonResult> GetLike(int post_id)
        {
            try
            {
                List<Posts> posts1 = await _postsServices.getlike(post_id);
                if (posts1 == null)
                    return Json(new { success = false, msg = "عذرا حدث خطا اثناء عملية جلب البيانات" });
                return Json(new { success = true, data = posts1 });
            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " BlogsController => likes_get");
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء جلب البيانات" });
            }
        }
        [HttpGet]
        public async Task<JsonResult> GetCommend(int post_id)
        {
            try
            {

                List<Posts> posts1 = await _postsServices.getcommend(post_id);
                if (posts1 == null)
                    return Json(new { success = false, msg = "عذرا حدث خطا اثناء عملية جلب البيانات" });

                return Json(new { success = true, data = posts1 });
            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " BlogsController => commend_get");
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء جلب البيانات" });
            }
           
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<JsonResult> GetNotification()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    List<Notification> Notification = await _postsServices.GetNotification(UserManger.Id);
                    if (Notification == null)
                        return Json(new { success = false, msg = "عذرا حدث خطا اثناء عملية جلب البيانات" });

                    return Json(new { success = true, data = Notification });
                }
                else
                return Json(new { success = false });
            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " BlogsController => GetNotification");
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء جلب البيانات" });
            }
          
        }
        
        [HttpGet]
        [AllowAnonymous]
        public async Task<JsonResult> GetCountNotification()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    Notification notification = await _postsServices.GetCountNotification(UserManger.Id);
                    return Json(new { success = true, data = notification });
                }
                else
                    return Json(new { success = false });
            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " BlogsController => GetCountNotification");
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء جلب البيانات" });
            }
        }
        [HttpPost]
        public async Task<JsonResult> SetNotification()
        {
            try
            {
                _postsServices.SetNotification(UserManger.Id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " BlogsController => SetNotification");
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء جلب البيانات" });
            }
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("Blogs/UploadFileAsync/{post_id}")]
        public async Task UploadFileAsync(int post_id)
        {
            try
            {
                var files = Request.Form.Files;
                foreach (IFormFile file in files)
                {
                    string imgex = Path.GetExtension(file.FileName);
                    if (imgex == ".jpg" || imgex == ".png")
                    {
                        var imgsave = Path.Combine(_hostEnvironment.WebRootPath, @"images\imag_post\", file.FileName);
                        var stream = new FileStream(imgsave, FileMode.Create);
                        await file.CopyToAsync(stream);
                        Images picture_ = new Images()
                        {
                            Image_path = file.FileName,
                            post_id = post_id
                        };
                        await _context.images.AddAsync(picture_);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " BlogsController => Upload_File");
            }

        }
        //[HttpPost]
        //[AllowAnonymous]
        //public async Task UploadFileAsync()
        //{
        //    IFormFileCollection file = Request.Form.Files;
        //    try
        //    {
        //        FilePath = "";
        //        long size = 0;

        //        var filename = ContentDispositionHeaderValue.Parse(file[0].ContentDisposition).FileName.Trim('"');
        //        FilePath = _hostEnvironment.WebRootPath + $@"\images\imag_post\{ filename}";
        //        size += file[0].Length;
        //        using (FileStream fs = System.IO.File.Create(FilePath))
        //        {
        //            file[0].CopyTo(fs);
        //            fs.Flush();
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        await log.WriteAsync(ex, " BlogsController => Upload_File");
        //    }
        //}
        [HttpPost]
        public async Task<JsonResult>   PostPosts(Posts posts)
        {
            try
            { posts.date = DateTime.UtcNow;
            posts.user_id = UserManger.Id;
            posts.post_id2 = 0;
            posts.like_bit = false;
            posts.count_like = 0;
            posts.count_comment = 0;
                posts.image = "";// _hostEnvironment.WebRootPath + $@"\images\imag_post" + posts.image.Substring(posts.image.LastIndexOf("\\"));

                await _context.Posts.AddAsync(posts);
                await _context.SaveChangesAsync();
                Posts posts1 = await _postsServices.getlastid();
                return Json(new { success = true, data = posts1, msg = "تم انشاء المنشور  بنجاح  يرجى متابعه صفحتك لعرض المنشور" });
            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " BlogsController => Posts_insert");
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء عملية انشاء المنشور" });
            }

        }
        [HttpPost]
        public async Task<JsonResult> UpdatePosts(Posts posts)
        {
            try
            {
                Posts posts2 = await _postsServices.GetPostsId(postManger);
                posts2.image = "";//_hostEnvironment.WebRootPath + $@"\images\imag_post" + posts.image.Substring(posts.image.LastIndexOf("\\"));

                posts2.title = posts.title;
                posts2.price = posts.price;
                posts2.date = DateTime.UtcNow;
                posts2.Model = posts.Model;
                posts2.address = posts.address;
                posts2.commend = posts.commend;
                posts2.Company_id = posts.Company_id;
                posts2.phone = posts.phone;
                posts2.Type_id = posts.Type_id;
                _context.Entry(posts2).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Json(new { success = true, data =posts2, msg = "تم نعديل المنشور  بنجاح  يرجى متابعه صفحتك لعرض المنشور" });
            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " BlogsController => updatePosts");
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء عملية تعديل المنشور" });
            }
        }
        [HttpPost]
        public async Task<JsonResult> PostCommend(int post_id, string commend)
        {
            
            try
            { 
                Posts posts1 = await _postsServices.Checkpost(post_id);
                if (posts1 == null)
                    return Json(new { success = false, msg = "عذرا حدث خطا اثناء عملية التعليق" });

                Posts posts = new Posts
                {
                    title = "",
                    commend = commend,
                    address = "",
                    price = 0,
                    phone = 0,
                    date = DateTime.UtcNow,
                    image = null,
                    Type_id = 0,
                    user_id = UserManger.Id,
                    post_id2 = post_id,
                    like_bit = false,
                    count_like = 0,
                };
                await _context.Posts.AddAsync(posts);
                await _context.SaveChangesAsync();

                posts1.count_comment = posts1.count_comment + 1;
                _context.Entry(posts1).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                Notification notification = new Notification
                {
                    user_id1 = posts1.user_id,
                    user_id2 = UserManger.Id,
                    post_id = posts1.post_id,
                    type_note = 2,
                    date = DateTime.UtcNow,
                    seen = false,
                };
                await _context.Notifications.AddAsync(notification);
                await _context.SaveChangesAsync();
                await _hubContext.Clients.All.SendAsync("displaynot", "");
                return Json(new { success = true, data = posts1, msg = "تم التعليق  بنجاح  " });
            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " BlogsController => Commend_insert");
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء عملية التعليق" });
            }

        }
        [HttpPost]
        public async Task<JsonResult> PostLike(int post_id)
        {
            try
            {
                Posts posts1 = await _postsServices.Checkpost(post_id);
                if (posts1 == null)
                    return Json(new { success = false, msg = "عذرا حدث خطا اثناء عملية الاعجاب" });
                Posts like = await _postsServices.checklike(post_id, UserManger.Id);
                if (like != null)
                {
                    _context.Posts.Remove(like);
                    await _context.SaveChangesAsync();
                    posts1.count_like = posts1.count_like - 1;
                    _context.Entry(posts1).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return Json(new { success = true, data = posts1, msg = "تم الغاء الاعجاب" });
                }
                Posts posts = new Posts
                {
                    title = "",
                    commend = "",
                    address = "",
                    price = 0,
                    phone = 0,
                    date = DateTime.UtcNow,
                    image = null,
                    Type_id = 0,
                    user_id =  UserManger.Id,
                    post_id2 = post_id,
                    like_bit = true,
                    count_like = 0,
                    count_comment = 0,
                };
                await _context.Posts.AddAsync(posts);
                await _context.SaveChangesAsync();
                posts1.count_like = posts1.count_like + 1;
                _context.Entry(posts1).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                Notification notification = new Notification
                {
                    user_id1 = posts1.user_id,
                    user_id2 = UserManger.Id,
                    post_id = posts1.post_id,
                    type_note = 1,
                    date = DateTime.UtcNow,
                    seen = false,
                };
                await _context.Notifications.AddAsync(notification);
                await _context.SaveChangesAsync();
                await _hubContext.Clients.All.SendAsync("displaynot", "");
                return Json(new { success = true, data = posts1, msg = "تم اعجاب بنجاح  " });
            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " BlogsController => like_insert");
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء عملية الاعجاب" });
            }

        }

        [HttpDelete]
        public async Task<JsonResult> Deletepost(int post_id)
        {
            try
            {
                Posts posts1 = await _postsServices.Checkpost(post_id);
                if (posts1 == null)
                    return Json(new { success = false, msg = "عذرا حدث خطا اثناء عملية الحذف" });

                _context.Entry(posts1).State = EntityState.Deleted;
                await _context.SaveChangesAsync();

                return Json(new { success = true, msg = "تم حذف المنشور بنجاح  " });
            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " BlogsController => delete_post");
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء عملية حذف المنشور" });
            }
        }
        [HttpDelete]
        public async Task<JsonResult> Deletenotifcation(int notifcation_id)
        {
            try
            {
                Notification notification = await _postsServices.Checknot(notifcation_id);
                if (notification == null)
                    return Json(new { success = false, msg = "عذرا حدث خطا اثناء عملية الحذف" });

                _context.Entry(notification).State = EntityState.Deleted;
                await _context.SaveChangesAsync();

                return Json(new { success = true, msg = "تم حذف الاشعار بنجاح  " });
            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " BlogsController => Deletenotifcation");
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء عملية حذف الاشعار" });
            }
        }

    }
}
