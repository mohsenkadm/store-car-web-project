using store_car_web_project.Models.Entity.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace store_car_web_project.Models.IServices
{
    public interface IPostsServices
    {
        public Task<Posts> CheckAccount(int post_id,int id);
        public Task<List<Posts>> getpost(int type_id);
        public Task<List<Posts>> getlike(int post_id);
        public Task<List<Posts>> getcommend(int post_id);
    }
}
