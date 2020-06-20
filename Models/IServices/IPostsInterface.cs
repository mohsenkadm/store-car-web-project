using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace store_car_web_project.Models.IServices
{
    public interface IPostsInterface
    {
        public  Task<object> Posts_get(int type_id);
        public Task<object> likes_get(int post_id);
        public Task<object> commend_get(int post_id);
        public Task Upload_FileAsync(IFormFileCollection file);
        public  Task<object> Posts_insert(string title, string commend, string address, int price, int phone, string image, string Company_id, int Model, int type_id);
        public Task<object> Commend_insert(int post_id, string commend);
        public Task<object> like_insert(int post_id);
        public Task<object> delete_post(int post_id);
    }
}
