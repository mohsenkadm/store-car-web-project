using Microsoft.AspNetCore.Mvc;
using store_car_web_project.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace store_car_web_project.Models.IServices
{
    public interface IUserInterface 
    {
        public  Task<object> Login(string username, string password);
        public Task<object> Register(string username, string password, string email);
        public Task<object> ConfirmAccount(string code, string username, string Email);
        public Task<object> Logout();
    }
}
