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
    public class BlogsController : MasterController
    {
        private readonly IPostsInterface _postsInterface;

        public BlogsController(PblogsContext context,IMemoryCache Cache, IPostsInterface postsInterface)
        {
            _Cache = Cache;
            _context = context;
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
        public JsonResult Posts_get(int type_id)
        {
            return Json(_postsInterface.Posts_get(type_id).Result);
        }
        [HttpGet]
        public JsonResult likes_get(int post_id)
        {
            return Json(_postsInterface.likes_get(post_id).Result);
        }
        [HttpGet]
        public JsonResult commend_get(int post_id)
        {
            return Json(_postsInterface.commend_get(post_id).Result);
        }
        [HttpPost]
        public void Upload_FileAsync()
        {
            IFormFileCollection file = Request.Form.Files;
            _postsInterface.Upload_FileAsync(file);

        }
        [HttpPost]
        public JsonResult Posts_insert(string title, string commend, string address, int price, int phone, string image,string Company_id,int Model, int type_id)
        {
            return Json(_postsInterface.Posts_insert(title,commend,address,price,phone,image,Company_id,Model,type_id).Result);


        }
        [HttpPost]
        public JsonResult Commend_insert(int post_id, string commend)
        {
            return Json(_postsInterface.Commend_insert(post_id, commend).Result);  

        }
        [HttpPost]
        public JsonResult like_insert(int post_id)
        {
            return Json(_postsInterface.like_insert(post_id).Result);

        }

        [HttpDelete]
        public JsonResult delete_post(int post_id)
        {
            return Json(_postsInterface.delete_post(post_id).Result);


        }

    }
}
