using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using System.Threading.Tasks;
using SearchRepo.Models;
using System.Net.Http;
using SearchRepo.Helpers;
using Newtonsoft.Json;

namespace SearchRepo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public void BookmarkRepository(Item item)
        {
            //should care about duplicates...later.
            if (this.Session["Bookmarked"] == null)
            {
                List<Item> bookmarked = new List<Item>();
                bookmarked.Add(item);
                this.Session["Bookmarked"] = bookmarked;
            }
            else
            {
                List<Item> bookmarked = this.Session["Bookmarked"] as List<Item>;
                bookmarked.Add(item);
                this.Session["Bookmarked"] = bookmarked;
            }
        }
        public ActionResult GetLastResults()
        {
            if (this.Session["CurrentResults"] == null)
            {
                return Json(new { data = String.Empty, errorCode = -3, errorDescription = "no results yet" });
            }
            else
            {
                List<Item> lastResult = this.Session["CurrentResults"] as List<Item>;
                return Json(new { data = lastResult, errorCode = 0, errorDescription = "OK" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetBookmarks()
        {
            if (this.Session["Bookmarked"] == null)
            {
                return Json(new { data = new List<Item>() , errorCode = -2 , errorDescription = "Bookmarks are empty" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<Item> bookmarked = this.Session["Bookmarked"] as List<Item>;
                
                this.Session["Bookmarked"] = bookmarked;
                return Json(new { data = bookmarked, errorCode = 0, errorDescription = "OK" }, JsonRequestBehavior.AllowGet);
            }
        }
        

            public async Task<ActionResult> RepositoryList(string query) {

            List<Item> repository = await HttpHelper.GetReposityList(query);
            if (repository == null)
            {
                this.Session["CurrentResults"] = null;
                return Json(new { data = String.Empty , errorCode = 0, errorDescription = "something went wrong" });
            }
            else
            {
                this.Session["CurrentResults"] = repository;
                return Json(new { data = repository , errorCode = 0 , errorDescription = "OK" }, JsonRequestBehavior.AllowGet);
            }
        } 
    }
}