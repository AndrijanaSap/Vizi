using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vizi.Models;

namespace Vizi.Controllers
{
    public class RatingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Ratings
        public ActionResult Index()
        {
            return View(db.Ratings.ToList());
        }

        public string addRating(Rating r)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId();
                r.UserId = currentUserId;
                db.Ratings.Add(r);
                db.SaveChanges();
            }
            return "Added";
        }
    }
}