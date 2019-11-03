using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vizi.Models;

namespace Vizi.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var model = db.Restaurants.ToList();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "About Vizi";

            return View();
        }
        public JsonResult GetSearchingData(string SearchValue)
        {
            List<Restaurant> StuList = new List<Restaurant>();
            StuList = GetRestaurants(SearchValue);
            List<SearchingViewModel> results = new List<SearchingViewModel>();
            int i = 0;
            foreach(var tmp in StuList)
            {
                SearchingViewModel t=new SearchingViewModel();
                t.Name = tmp.Name;
                t.Id = tmp.Id;
                results.Add(t);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
            
        }
        private List<Restaurant> GetRestaurants(string searchString)
        {
            return db.Restaurants.Where(a => a.Name.Contains(searchString))
                .ToList();
        }
    }
    public class SearchingViewModel
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
}