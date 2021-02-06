using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace store_car_web_project.Models.Entity.Security
{
    public class Posts
    {
        public int post_id { get; set; }
        public string title { get; set; }
        public string commend { get; set; }
        public string address { get; set; }
        public double price { get; set; }
        public decimal phone { get; set; }

        public DateTime? date { get; set; }
        public string image { get; set; }

        public string Company_id { get; set; }
        public int Model { get; set; }
        public int Type_id { get; set; }
        public int user_id { get; set; }
        public int post_id2 { get; set; }
        public bool like_bit { get; set; }
        public int count_comment { get; set; }
        
        public int count_like { get; set; }
        public string userName { get;  set; }
        public bool isliked { get; set; }
        public string imagepath { get; set; }
        public int imagecount { get; set; }
    }
}
