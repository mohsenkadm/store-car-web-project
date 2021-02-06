
using store_car_web_project.Models.Entity.Security;
using store_car_web_project.Models.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace store_car_web_project.Models.Services
{
    public class ChatServices :IChatServices
    {
        private readonly MasterService<Messag> _chatServices;
        public ChatServices()
        {
            _chatServices = new MasterService<Messag>();
        }

        public async Task<Messag> GetCountMessage(int user_id)
        {
            return await _chatServices.GetEntityAsync("security.GetCountNotification", new { user_id });
        }

        public async Task<Messag> GetMessage(int user_id)
        {
            return await _chatServices.GetEntityAsync("security.GetCountNotification", new { user_id });
        }

        public async void SetCountMessage(int user_id)
        {
            await _chatServices.RunSpAsync("security.setnotefication", new { user_id });
        }
    }
}
