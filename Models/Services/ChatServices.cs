
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

        public async Task<Messag> Checkmessage(int id)
        {
            return await _chatServices.GetEntityAsync("security.Checkmessage", new { id });
        }

        public async Task<Messag> GetCountMessage(int user_id)
        {
            return await _chatServices.GetEntityAsync("security.GetCountMessage", new { user_id });
        }
        public async Task<List<Messag>> GetMessage(int user_id)
        {
            return await _chatServices.GetEntityListAsync("security.GetMessage", new { user_id });
        }

        public async Task<List<Messag>> GetMessagechat(int User_reciver_id, int User_sender_id)
        {
            return await _chatServices.GetEntityListAsync("security.GetMessagechat", new { User_reciver_id, User_sender_id });
        }

        public async void SetCountMessage(int user_id)
        {
            await _chatServices.RunSpAsync("security.setMessage", new { user_id });
        }
        public async void SetSeenMessage(int User_reciver_id,int User_sender_id)
        {
            await _chatServices.RunSpAsync("security.setseenmessage", new { User_reciver_id, User_sender_id });
        }
    }
}
