using store_car_web_project.Models.Entity.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace store_car_web_project.Models.IServices
{
   public interface IUserServices
    {
        public  Task<Users> Login_chek(string username, string password);
        public Task<Users> getbyid(int id);
        public Task<Users> CheckUserinfo(string username, string Email);
        public Task<Users> checkConfirmAccount(string code,string username, string Email);
        public List<Users> getusers();
        public Task<Users> GetUserInfo(int Id);

    }
}
