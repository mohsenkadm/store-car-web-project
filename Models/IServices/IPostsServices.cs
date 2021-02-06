using store_car_web_project.Models.Entity.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace store_car_web_project.Models.IServices
{
    public interface IPostsServices
    {
        public Task<Posts> CheckAccount(int post_id);
        public Task<Posts>   checklike(int post_id,int id);
        public Task<List<Posts>> getpost(int type_id,int user_manager);
        public Task<Posts> GetPostsId(int post_id);
        public Task<List<Posts>> getlike(int post_id);
        public Task<List<Posts>> getcommend(int post_id);
        public Task<List<Notification>> GetNotification(int user_id1);
        public List<Posts> getpostall();
        public Task<Posts> getlastid();
        public Task<List<Posts>> getpostProfile(int user_id,int user_manager);
        public Task<List<Posts>> getsearchpost(string where_code,int user_manager);
        public void SetNotification(int user_id1);
        public Task<Notification> GetCountNotification(int user_id);
    }
}
