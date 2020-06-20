using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using store_car_web_project.Controllers;
using store_car_web_project.Models.Entity.Security;
using store_car_web_project.Models.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace store_car_web_project.Models.Services
{
    public class PostsInterface : MasterController, IPostsInterface
    {
        private readonly IPostsServices _postsServices;
        private readonly IWebHostEnvironment _hostEnvironment;
        private string FilePath;
        public PostsInterface(PblogsContext context, IMemoryCache Cache, IPostsServices postsServices, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _Cache = Cache;
            _postsServices = postsServices;
            this._hostEnvironment = hostEnvironment;
        }
        public async Task<object> GetCommend(int post_id)
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
                await log.WriteAsync(ex, " PostsInterface => commend_get");
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء جلب البيانات" });
            }
        }

        public async Task<object> PostCommend(int post_id, string commend)
        {
            try
            {
                Posts posts1 = await _postsServices.CheckAccount(post_id, UserManger.Id);
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
                    post_id2 = posts1.post_id,
                    like_bit = false,
                    count_like = 0,
                };

                await _context.Posts.AddAsync(posts);
                await _context.SaveChangesAsync();

                posts1.count_comment = posts1.count_comment + 1;
                _context.Entry(posts1).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Json(new { success = true, msg = "تم التعليق  بنجاح  " });
            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " PostsInterface => Commend_insert");
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء عملية التعليق" });
            }

        }

        public async Task<object> Deletepost(int post_id)
        {
            try
            {
                Posts posts1 = await _postsServices.CheckAccount(post_id, UserManger.Id);
                if (posts1 == null)
                    return Json(new { success = false, msg = "عذرا حدث خطا اثناء عملية الحذف" });

                _context.Entry(posts1).State = EntityState.Deleted;
                await _context.SaveChangesAsync();

                return Json(new { success = true, msg = "تم حذف المنشور بنجاح  " });
            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " PostsInterface => delete_post");
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء عملية حذف المنشور" });
            }
        }

        public async Task<object> GetLike(int post_id)
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
                await log.WriteAsync(ex, " PostsInterface => likes_get");
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء جلب البيانات" });
            }
        }

        public async Task<object> PostLike(int post_id)
        {
            try
            {
                Posts posts1 = await _postsServices.CheckAccount(post_id, UserManger.Id);
                if (posts1 == null)
                    return Json(new { success = false, msg = "عذرا حدث خطا اثناء عملية الاعجاب" });

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
                    user_id = UserManger.Id,
                    post_id2 = posts1.post_id,
                    like_bit = true,
                    count_like = 0,
                    count_comment = 0,
                };

                await _context.Posts.AddAsync(posts);
                await _context.SaveChangesAsync();

                posts1.count_like = posts1.count_like + 1;
                _context.Entry(posts1).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Json(new { success = true, msg = "تم اعجاب بنجاح  " });
            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " PostsInterface => like_insert");
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء عملية الاعجاب" });
            }

        }

        public async Task<object> GetPosts(int type_id)
        {
            try
            {
                List<Posts> posts1 = await _postsServices.getpost(type_id);
                if (posts1 == null)
                    return Json(new { success = false, msg = "عذرا حدث خطا اثناء عملية جلب البيانات" });

                return Json(new { success = true, data = posts1 });
            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " PostsInterface => Posts_get");
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء جلب البيانات" });
            }
        }

        public async Task<object> PostPosts(Posts posts,string image)
        {
            try
            {
                posts.image = _hostEnvironment.WebRootPath + $@"\images\imag_post" + image.Substring(image.LastIndexOf("\\"));
                
                await _context.Posts.AddAsync(posts);
                await _context.SaveChangesAsync();
                return Json(new { success = true, msg = "تم انشاء المنشور  بنجاح  يرجى متابعه صفحتك لعرض المنشور" });
            }
            catch (Exception ex)
            {
                await log.WriteAsync(ex, " PostsInterface => Posts_insert");
                return Json(new { success = false, msg = "عذرا حدث خطا اثناء عملية انشاء المنشور" });
            }
        }

        public async Task UploadFileAsync(IFormFileCollection file)
        {

            try
            {
                FilePath = "";
                long size = 0; 
              
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
                await log.WriteAsync(ex, " PostsInterface => Upload_File");
            }
        }
        private object Json(object p)
        {
            return p;
        }
    }
}
