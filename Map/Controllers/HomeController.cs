using Map.Models;
using Server.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Map.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var entities = new ServerLogEntities();

            return View(entities.ServerLogs.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult GetDataBaseData()
        {
            var entities = new ServerLogEntities();
            return Json(entities.ServerLogs.ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}