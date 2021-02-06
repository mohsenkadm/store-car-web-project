using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace store_car_web_project.Models.Entity.Security
{
    public class Messag
    {
        public int Id { get; set; }
        public string Message_txt { get; set; }
        public DateTime? Message_date { get; set; }
        public int User_sender_id { get; set; }
        public string User_name_s { get; set; }
        public int User_reciver_id { get; set; }
        public string User_name_r { get; set; }
        public bool seen { get; set; }
        public int count { get; set; }


    }
}
