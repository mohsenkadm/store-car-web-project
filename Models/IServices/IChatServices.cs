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
       public Task<Messag> GetCountMessage(int user_id);
       public Task<Messag> GetMessage(int user_id);
    }
}
