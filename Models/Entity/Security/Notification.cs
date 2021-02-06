using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace store_car_web_project.Models.Entity.Security
{
    public class Notification
    {
        public int Id { get; set; }
        public int user_id1 { get; set; }
        public int user_id2 { get; set; }
        public int type_note { get; set; }
        public int post_id { get; set; }
        public DateTime? date { get; set; }
        public string userName { get; set; }
        public bool seen { get; set; }
        public int count { get; set; }
    }
}
