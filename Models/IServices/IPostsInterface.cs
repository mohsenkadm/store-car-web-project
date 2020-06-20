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
        public  Task<object> Postsget(int type_id);
        public Task<object> likesget(int post_id);
        public Task<object> Commendget(int post_id);
        public Task UploadFileAsync(IFormFileCollection file);
        public  Task<object> Postsinsert(Posts posts,string image);
        public Task<object> Commendinsert(int post_id, string commend);
        public Task<object> likeinsert(int post_id);
        public Task<object> Deletepost(int post_id);
    }
}
