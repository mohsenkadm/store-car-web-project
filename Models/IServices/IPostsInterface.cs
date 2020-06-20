using Microsoft.AspNetCore.Http;
using store_car_web_project.Models.Entity.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace store_car_web_project.Models.IServices
{
    public interface IPostsInterface
    {
        public  Task<object> GetPosts(int type_id);
        public Task<object> GetLike(int post_id);
        public Task<object> GetCommend(int post_id);
        public Task UploadFileAsync(IFormFileCollection file);
        public  Task<object> PostPosts(Posts posts,string image);
        public Task<object> PostCommend(int post_id, string commend);
        public Task<object> PostLike(int post_id);
        public Task<object> Deletepost(int post_id);
    }
}
