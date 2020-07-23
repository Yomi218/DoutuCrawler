using DoutuCrawler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Show.Controllers
{
    public class InfoController : Controller
    {
        // GET: Show
        public ActionResult Index()
        {
            DoutuEntities entity = new DoutuEntities();
            var list = entity.Set<Info>().ToList();
            ViewBag.InfoList = list;
            return View();
        }
    }
}