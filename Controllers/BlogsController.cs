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
        public JsonResult Postsget(int type_id)
        {
            return Json(_postsInterface.Postsget(type_id).Result);
        }
        [HttpGet]
        public JsonResult likesget(int post_id)
        {
            return Json(_postsInterface.likesget(post_id).Result);
        }
        [HttpGet]
        public JsonResult Commendget(int post_id)
        {
            return Json(_postsInterface.Commendget(post_id).Result);
        }
        [HttpPost]
        public void UploadFileAsync()
        {
            IFormFileCollection file = Request.Form.Files;
            _postsInterface.UploadFileAsync(file);

        }
        [HttpPost]
        public JsonResult Postsinsert(Posts posts)
        {
            posts.date = DateTime.UtcNow;
            posts.user_id = 1;
            posts.post_id2 = 0;
            posts.like_bit = false;
            posts.count_like = 0;
            posts.count_comment = 0;
            return Json(_postsInterface.Postsinsert(posts, posts.image).Result);


        }
        [HttpPost]
        public JsonResult Commendinsert(int post_id, string commend)
        {
            return Json(_postsInterface.Commendinsert(post_id, commend).Result);  

        }
        [HttpPost]
        public JsonResult likeinsert(int post_id)
        {
            return Json(_postsInterface.likeinsert(post_id).Result);

        }

        [HttpDelete]
        public JsonResult Deletepost(int post_id)
        {
            return Json(_postsInterface.Deletepost(post_id).Result);


        }

    }
}
