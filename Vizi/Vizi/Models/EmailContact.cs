using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vizi.Models
{
    public class EmailContact
    {
        public string PhoneNumber { get; set; }
        public int People { get; set; }
        public int RestaurantId { get; set; }

        public string Hour { get; set; }
    }
}