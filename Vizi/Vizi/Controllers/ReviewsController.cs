using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vizi.Models;

namespace Vizi.Controllers
{
    [Authorize]
    public class ReviewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reviews
        public ActionResult Index()
        {
            return View(db.Reviews.ToList());
        }

        // GET: Reviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // GET: Reviews/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RestaurantId,Body")] Review review)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
                review.UserId = currentUser.Id;
                string ime = db.Restaurants.Find(Convert.ToInt32(review.RestaurantId)).Name;
                review.RestaurantName = ime;

                db.Reviews.Add(review);
                db.SaveChanges();
                return RedirectToAction("Details","Restaurants",new { id=review.RestaurantId});
            }

            return View(review);
        }
        
        public ActionResult getUsername(string userId)
        {
            ApplicationUser user = db.Users.Find(userId);
            return Content(user.UserName);

        }
        [Authorize(Roles = ("User, Manager"))]
        public JsonResult AjaxReview([Bind(Include = "Id,RestaurantId,Body")] Review review)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
                review.UserId = currentUser.Id;
                string ime = db.Restaurants.Find(Convert.ToInt32(review.RestaurantId)).Name;
                review.RestaurantName = ime;
                review.Timestamp = DateTime.Now.ToString();
                db.Reviews.Add(review);
                db.SaveChanges();
            }
            return Json(review);
            }

        // GET: Reviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RestaurantId,Body")] Review review)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
                review.UserId = currentUser.Id;
                review.RestaurantName = db.Restaurants.Find(review.RestaurantId).Name;

                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Restaurants", new { id = review.RestaurantId });
            }
            return View(review);
        }
        
        [Authorize(Roles = ("Admin, Manager, User"))]
        public string DeleteReview(int? id)
        {
            if (id != null) {
                string currentUserId = User.Identity.GetUserId();
                Review review = db.Reviews.Find(id);
                if (review.UserId == currentUserId)
                {
                    int beforeLength = db.Reviews.ToList().Count;
                    db.Reviews.Remove(review);
                    db.SaveChanges();
                    int afterLength = db.Reviews.ToList().Count;
                    if (beforeLength - 1 == afterLength)
                    {
                        return "Removed";
                    }
                }
                else
                {
                    return "You have no authorization";
                }
            }
            return "Could not remove comment";
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Reviews.Find(id);
            db.Reviews.Remove(review);
            db.SaveChanges();
            return RedirectToAction("Details", "Restaurants", new { id = review.RestaurantId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
