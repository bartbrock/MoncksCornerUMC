using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MoncksCornerUMC.Models;

namespace MoncksCornerUMC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Worship()
        {
            ViewBag.Message = "Your Worship page.";

            return View();
        }

        public ActionResult Events()
        {
            ViewBag.Message = "Your Events page.";
            string eventGroupID = "SpecialEvent";
            string numDays = "14";

            return View(RssReader.GetRssFeed());
        }

        public ActionResult Ministries()
        {
            ViewBag.Message = "Your Ministries page.";

            return View();
        }

        public ActionResult Children()
        {
            ViewBag.Message = "Your Children page.";

            return View();
        }

        public ActionResult Youth()
        {
            ViewBag.Message = "Your Youth page.";

            return View();
        }

        public ActionResult Missions()
        {
            ViewBag.Message = "Your Missions page.";

            return View();
        }

        public ActionResult Publications()
        {
            ViewBag.Message = "Your Publications page.";

            return View();
        }

        public ActionResult Giving()
        {
            ViewBag.Message = "Your Giving page.";

            return View("GivingCountDown");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your About page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}