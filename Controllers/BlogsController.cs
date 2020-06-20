using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
    public class BlogsController : Controller
    {
        private readonly IPostsInterface _postsInterface;

        public BlogsController( IPostsInterface postsInterface)
        {
            _postsInterface = postsInterface;
            
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
        public JsonResult GetPosts(int type_id)
        {
            return Json(_postsInterface.GetPosts(type_id).Result);
        }
        [HttpGet]
        public JsonResult GetLike(int post_id)
        {
            return Json(_postsInterface.GetLike(post_id).Result);
        }
        [HttpGet]
        public JsonResult GetCommend(int post_id)
        {
            return Json(_postsInterface.GetCommend(post_id).Result);
        }
        [HttpPost]
        public void UploadFileAsync()
        {
            IFormFileCollection file = Request.Form.Files;
            _postsInterface.UploadFileAsync(file);

        }
        [HttpPost]
        public JsonResult   PostPosts(Posts posts)
        {
            posts.date = DateTime.UtcNow;
            posts.user_id = 1;
            posts.post_id2 = 0;
            posts.like_bit = false;
            posts.count_like = 0;
            posts.count_comment = 0;
            return Json(_postsInterface.PostPosts(posts, posts.image).Result);


        }
        [HttpPost]
        public JsonResult PostCommend(int post_id, string commend)
        {
            return Json(_postsInterface.PostCommend(post_id, commend).Result);  

        }
        [HttpPost]
        public JsonResult PostLike(int post_id)
        {
            return Json(_postsInterface.PostLike(post_id).Result);

        }

        [HttpDelete]
        public JsonResult Deletepost(int post_id)
        {
            return Json(_postsInterface.Deletepost(post_id).Result);


        }

    }
}
