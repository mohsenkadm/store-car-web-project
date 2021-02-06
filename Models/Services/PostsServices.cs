using Microsoft.IdentityModel.Tokens;
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
        private readonly MasterService<Notification> _noteService;
        private readonly MasterService<Images> _imageService;
        public PostsServices()
        {
            _postService = new MasterService<Posts>();
            _noteService = new MasterService<Notification>();
            _imageService = new MasterService<Images>();
        }
        public async Task<Posts> CheckAccount(int post_id)
        {
            return await _postService.GetEntityAsync("security.post_checkAccount", new { post_id });
        }
        public async Task<Posts> checklike(int post_id,int user_id)
        {
            return await _postService.GetEntityAsync("security.checklike", new { post_id, user_id });
        }

        public async Task<List<Posts>> getcommend(int post_id)
        {
            return await _postService.GetEntityListAsync("security.getcommend", new { post_id });
        }
        public async Task<List<Notification>> GetNotification(int user_id1)
        {
            return await _noteService.GetEntityListAsync("security.getNote", new { user_id1 });
        }

        public async Task<List<Posts>> getlike(int post_id)
        {
            return await _postService.GetEntityListAsync("security.getlike", new { post_id });
        }

        public async Task<List<Posts>> getpost(int type_id,int user_manager)
        {
            return await _postService.GetEntityListAsync("security.getpost", new { type_id, user_manager });
        }
        public async Task<Posts> GetPostsId(int post_id)
        {
            return await _postService.GetEntityAsync("security.GetPostsId", new { post_id });
        }
        public async Task<Posts> getlastid()
        {
            return await _postService.GetEntityAsync("security.getlastid", null);
        }
        public  List<Posts> getpostall()
        {
            return _postService.GetEntityList("security.getpostall", null);
        }

        public async Task<List<Posts>> getpostProfile(int user_id,int user_manager)
        {
            return await _postService.GetEntityListAsync("security.getpostProfile", new { user_id, user_manager });
        }

        public async Task<List<Posts>> getsearchpost(string where_code,int user_manager)
        {
            return await _postService.GetEntityListAsync("security.getsearchpost", new { where_code , user_manager });
        }

        public async void SetNotification(int user_id1)
        {
           await _postService.RunSpAsync("security.setnotefication", new { user_id1 });
        }

        public async  Task<Notification> GetCountNotification(int user_id)
        {
            return await  _noteService.GetEntityAsync("security.GetCountNotification", new { user_id });
        }

        public async Task<List<Images>> getimages(int post_id)
        {
            return await _imageService.GetEntityListAsync("security.getimages", new { post_id });
        }
    }
}
