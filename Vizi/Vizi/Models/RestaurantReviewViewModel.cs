using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vizi.Models
{
    public class RestaurantReviewViewModel
    {
        public List<Review> ListOfReviews { get; set; }

        public List<Rating> ListOfRatings { get; set; }
        public Restaurant CurrentRestaurant { get; set; }
        public string CurrentUserId { get; set; }

        public RestaurantReviewViewModel()
        {
            ListOfRatings = new List<Rating>();
            ListOfReviews = new List<Review>();
        }
    }
}