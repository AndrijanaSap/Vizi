using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vizi.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public string UserId { get; set; }
        public int rating { get; set; }
    }
}