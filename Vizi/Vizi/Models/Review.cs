using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vizi.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public string Timestamp { get; set; }
        public string Body { get; set; }
    }
}