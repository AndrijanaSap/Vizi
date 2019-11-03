using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Vizi.Models;

namespace Vizi.Controllers
{
    public class RestaurantsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Restaurants
        public ActionResult Index()
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);

            var model = (from r in db.Restaurants
                         where r.ManagerId == currentUser.Id
                         select r).ToList();
            return View(model);
        }



    
        // GET: Restaurants/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = db.Restaurants.Find(id);
            string ime = db.Restaurants.Find(id).Name;
            var listOfReviews = (from r in db.Reviews
                                 where r.RestaurantName == ime
                                 select r).ToList();
            var listOfRatings = (from r in db.Ratings
                                 where r.RestaurantId == id
                                 select r).ToList();

            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);

            RestaurantReviewViewModel model = new RestaurantReviewViewModel();
            model.ListOfReviews = listOfReviews;
            model.ListOfRatings = listOfRatings;
            model.CurrentRestaurant = restaurant;
            model.CurrentUserId = currentUser.Id;

            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        public FileContentResult getImg(int id)
        {
            Restaurant restaurant = db.Restaurants.Find(id);
            if (restaurant.Picture != null)
            {
                return new FileContentResult(restaurant.Picture, "image/jpg");
            }
            else
            {
                return null;
            }

        }

        public FileContentResult getSlider(int id)
        {
            Restaurant restaurant = db.Restaurants.Find(id);
            if (restaurant.SidePicture1 != null)
            {
                return new FileContentResult(restaurant.SidePicture1, "image/jpg");
            }
            else
            {
                return null;
            }
        }

        public FileContentResult getSlider2(int id)
        {
            Restaurant restaurant = db.Restaurants.Find(id);
            if (restaurant.SidePicture2 != null)
            {
                return new FileContentResult(restaurant.SidePicture2, "image/jpg");
            }
            else
            {
                return null;
            }
        }

        public FileContentResult getSlider3(int id)
        {
            Restaurant restaurant = db.Restaurants.Find(id);
            if (restaurant.SidePicture3 != null)
            {
                return new FileContentResult(restaurant.SidePicture3, "image/jpg");
            }
            else
            {
                return null;
            }
        }

        public FileContentResult getSlider4(int id)
        {
            Restaurant restaurant = db.Restaurants.Find(id);
            if (restaurant.SidePicture4 != null)
            {
                return new FileContentResult(restaurant.SidePicture4, "image/jpg");
            }
            else
            {
                return null;
            }
        }

      
        public void makeReservation(EmailContact ec)
        {
            
            System.Diagnostics.Debug.WriteLine("tukaaaaaaaaaaaaaaaaaaaa");
            var body = "<html><body style='background-color:orange; font-family: Arial,sans-serif; text-align:center;'><h1>Reservation</h1><br><p>A reservation was made for your restaurant, {0}, at {1} for {2} people. Please contact {3} to confirm reservation.</p><p>Vizi team</p></body><html>";
            var message = new MailMessage();
            string userId = User.Identity.GetUserId();
            ApplicationUser au = db.Users.Find(userId);
            var restaurant = (from r in db.Restaurants
                              where r.Id == ec.RestaurantId
                              select r).FirstOrDefault();
            var manager = (from r in db.Users
                           where r.Id == restaurant.ManagerId
                           select r).FirstOrDefault();
            System.Diagnostics.Debug.WriteLine("tukaaaaaaaaaaaaaaaaaaaa");
            message.To.Add(new MailAddress(manager.Email));
            message.From = new MailAddress("noreply.vizi@gmail.com", "noreply");
            message.Subject = "Reservation for " + restaurant.Name;
            message.Body = string.Format(body, restaurant.Name, ec.Hour, ec.People, ec.PhoneNumber);
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "noreply.vizi@gmail.com",
                    Password = "Ane123!@#"
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Send(message);
            }

        }

        // GET: Restaurants/Create
        [Authorize(Roles = "Manager, Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Restaurants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Address,Telephone,Email")] Restaurant restaurant, HttpPostedFileBase file, HttpPostedFileBase sidefile1,
            HttpPostedFileBase sidefile2, HttpPostedFileBase sidefile3, HttpPostedFileBase sidefile4)
        {
            if (ModelState.IsValid)
            {
                // Custom10Code
                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
                restaurant.ManagerId = currentUser.Id;
                int slikaSize = file.ContentLength;
                int sideSlikaSize1 = sidefile1.ContentLength;
                int sideSlikaSize2 = sidefile2.ContentLength;
                int sideSlikaSize3 = sidefile3.ContentLength;
                int sideSlikaSize4 = sidefile4.ContentLength;
                using (var br = new BinaryReader(file.InputStream))
                {
                    restaurant.Picture = br.ReadBytes(slikaSize);
                }

                using (var br = new BinaryReader(sidefile1.InputStream))
                {
                    restaurant.SidePicture1 = br.ReadBytes(sideSlikaSize1);
                }
                using (var br = new BinaryReader(sidefile2.InputStream))
                {
                    restaurant.SidePicture2 = br.ReadBytes(sideSlikaSize2);
                }
                using (var br = new BinaryReader(sidefile3.InputStream))
                {
                    restaurant.SidePicture3 = br.ReadBytes(sideSlikaSize3);
                }
                using (var br = new BinaryReader(sidefile4.InputStream))
                {
                    restaurant.SidePicture4 = br.ReadBytes(sideSlikaSize4);
                }

                db.Restaurants.Add(restaurant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(restaurant);
        }

        // GET: Restaurants/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = db.Restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            
            return View(restaurant);
        }

        // POST: Restaurants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager, Admin")]
        public ActionResult Edit([Bind(Include = "Id,Name,Address,Telephone,Email")] Restaurant restaurant, HttpPostedFileBase file, HttpPostedFileBase sidefile1,
            HttpPostedFileBase sidefile2, HttpPostedFileBase sidefile3, HttpPostedFileBase sidefile4)
        {
            if (ModelState.IsValid)
            {
                // Custom10Code
                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
                restaurant.ManagerId = currentUser.Id;
                int slikaSize = file.ContentLength;
                int sideSlikaSize1 = sidefile1.ContentLength;
                int sideSlikaSize2 = sidefile2.ContentLength;
                int sideSlikaSize3 = sidefile3.ContentLength;
                int sideSlikaSize4 = sidefile4.ContentLength;
                using (var br = new BinaryReader(file.InputStream))
                {
                    restaurant.Picture = br.ReadBytes(slikaSize);
                }

                using (var br = new BinaryReader(sidefile1.InputStream))
                {
                    restaurant.SidePicture1 = br.ReadBytes(sideSlikaSize1);
                }
                using (var br = new BinaryReader(sidefile2.InputStream))
                {
                    restaurant.SidePicture2 = br.ReadBytes(sideSlikaSize2);
                }
                using (var br = new BinaryReader(sidefile3.InputStream))
                {
                    restaurant.SidePicture3 = br.ReadBytes(sideSlikaSize3);
                }
                using (var br = new BinaryReader(sidefile4.InputStream))
                {
                    restaurant.SidePicture4 = br.ReadBytes(sideSlikaSize4);
                }

                db.Entry(restaurant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(restaurant);
        }

        // DELETE: Restaurants/Delete/5
        [HttpPost]
        public void DeleteAjax(int id)
        {
            // Custom Code
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);

            string ime = db.Restaurants.Find(id).Name;
            var listOfReviews = (from r in db.Reviews
                                 where r.RestaurantName == ime
                                 select r).ToList();
            foreach(Review r in listOfReviews)
            {
                db.Reviews.Remove(r);
            }
            Restaurant restaurant = db.Restaurants.Find(id);
            db.Restaurants.Remove(restaurant);
            db.SaveChanges();
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
