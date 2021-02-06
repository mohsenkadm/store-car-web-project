using store_car_web_project.Models.Entity.Security;
using store_car_web_project.Models.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace store_car_web_project.Models.Services
{
    public class UserServices : IUserServices
    {
        private readonly MasterService<Users> _userService;
        public UserServices()
        {
            _userService = new MasterService<Users>();
        }

        public async Task<Users> Login_chek(string username, string password)
        {
            return await _userService.GetEntityAsync("security.user_login", new { username, password });
        }
        public async Task<Users> CheckUserinfo(string username, string Email)
        {
            return await _userService.GetEntityAsync("security.user_CheckUserinfo", new { username, Email });
        }

        public async Task<Users> checkConfirmAccount(string code, string username, string Email)
        {
            return await _userService.GetEntityAsync("security.user_checkConfirmAccount", new { code, username, Email });
        }

        public async Task<Users> getbyid(int id)
        {
            return await _userService.GetEntityAsync("security.select_user", new { id });
        }

        public  List<Users> getusers()
        {
            return  _userService.GetEntityList("security.getuserall", null);
        }

        public async Task<List<Users>> GetUserInfo(int Id)
        {
            return await _userService.GetEntityListAsync("security.getuserinfo", new { Id });
        }
    }
}
