using SLD521SA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SLD521SA.Controllers
{
    public class HomeController : Controller
    {

        private sld521_saEntities db = new sld521_saEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Papers()
        {
            var papers = db.Papers;
            return View(papers);
        }

        public ActionResult News()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}