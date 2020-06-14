using store_car_web_project.Models.Entity.Security;
using store_car_web_project.Models.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace store_car_web_project.Models.Services
{
    public class PostsServices : IPostsServices
    {
        private readonly MasterService<Posts> _postService;
        public PostsServices()
        {
            _postService = new MasterService<Posts>();
        }
        public async Task<Posts> CheckAccount(int post_id,int user_id)
        {
            return await _postService.GetEntityAsync("security.post_checkAccount", new { post_id, user_id });
        }

        public async Task<List<Posts>> getcommend(int post_id)
        {
            return await _postService.GetEntityListAsync("security.getcommend", new { post_id });
        }

        public async Task<List<Posts>> getlike(int post_id)
        {
            return await _postService.GetEntityListAsync("security.getlike", new { post_id });
        }

        public async Task<List<Posts>> getpost(int type_id)
        {
            return await _postService.GetEntityListAsync("security.getpost", new { type_id});
        }
    }
}
