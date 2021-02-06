using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
        public int _TypeId;
        public int _userId2;
        public int _post_id;
        private readonly MasterService<Users> _userService;
        private readonly MasterService<Posts> _PostsService;
        private readonly MasterService<Messag> _ChatService;
        private readonly JsonWebToken jwt;
        public IHubContext<Signalr> _hubContext;
        public MasterController()
        {
            log = new Logger();
            jwt = new JsonWebToken();
            _userService = new MasterService<Users>();
            _PostsService = new MasterService<Posts>();
            _ChatService = new MasterService<Messag>();
            //if (_UserId == 0) getid();
        }
        public Users UserManger
        {
            get {
                if (_UserId == 0) getid();
                if (_Cache.TryGetValue("User"+_UserId, out Users users))
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

        public int TypeManger
        {
            get
            {
                if (_UserId == 0) getid();
                if (_Cache.TryGetValue("Posts" + _UserId, out  int TypeId))
                {
                    return TypeId;
                }
                else
                    return 0;
            }
            set
            {
                if (_UserId == 0) getid();
                _Cache.Set("Posts"+_UserId, value, TimeSpan.FromMinutes(10));
            }
        }
        public int user_idManger
        {
            get
            {
                if (_UserId == 0) getid();
                if (_Cache.TryGetValue("user_id"+_UserId, out int userId2))
                {
                    return userId2;
                }
                else
                    return 0;
            }
            set
            {
                if (_UserId == 0) getid();
                _Cache.Set("user_id"+ _UserId, value, TimeSpan.FromMinutes(10));
            }
        }
        public int postManger
        {
            get
            {
                if (_UserId == 0) getid();
                if (_Cache.TryGetValue("post_id"+_UserId, out int _post_id))
                {
                    return _post_id;
                }
                else
                    return 0;
            }
            set
            {
                if (_UserId == 0) getid();
                _Cache.Set("post_id"+_UserId, value, TimeSpan.FromMinutes(1));
            }
        }

        private Users Resetuserinfo()
        {
            try
            {
                //set cash
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
            if(Request!=null)
            _UserId = jwt.ValidateToken(Request.Headers["Authorization"].ToString());
        }
    }
}





