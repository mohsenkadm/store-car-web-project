using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using store_car_web_project.Classes;
using store_car_web_project.Models;
using store_car_web_project.Models.Entity.Security;

namespace store_car_web_project.Controllers
{
    public class MasterController : Controller
    {
        public  PblogsContext _context;
        public readonly Logger log;
        public IMemoryCache _Cache;
        public int _UserId;
        private readonly MasterService<Users> _userService;
        private readonly JsonWebToken jwt;
        public MasterController()
        {
            log = new Logger();
            jwt = new JsonWebToken();
            _userService = new MasterService<Users>();
            if (_UserId == 0) getid();
        }
        public Users UserManger
        {
            get {
                if (_UserId == 0) getid();
                if (_Cache.TryGetValue("User" + _UserId, out Users users))
                {
                    return users;
                }
                else
                return Resetuserinfo();
            }
            //vaule  is  user data
            set
            {
                if (_UserId == 0) getid();
                _Cache.Set("User" + _UserId, value, TimeSpan.FromDays(1));
            }
        }
        private Users Resetuserinfo()
        {
            try
            {///set cash
               UserManger = _userService.GetEntity("security.select_user", new { id = _UserId });

                    //get cash
                    return UserManger;
            }
            catch (Exception ex)
            {
                new Logger().Write(ex, "MasterController => Resetuserinfo");
                throw;
            }
        }
        protected void getid()
        {
         //   _UserId = jwt.ValidateToken(Request.Headers["Authorization"].ToString());
        }
    }
}





