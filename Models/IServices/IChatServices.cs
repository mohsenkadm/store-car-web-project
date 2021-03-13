using store_car_web_project.Models.Entity.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace store_car_web_project.Models.IServices
{
    public interface IChatServices
    {
       public void SetCountMessage(int user_id);
        public Task<Messag> Checkmessage(int post_id);
        public Task<Messag> GetCountMessage(int user_id);
       public Task<List<Messag>> GetMessage(int user_id);
     public   Task<List<Messag>> GetMessagechat(int User_reciver_id, int User_sender_id);
        public void SetSeenMessage(int User_reciver_id, int User_sender_id);
    }
}
