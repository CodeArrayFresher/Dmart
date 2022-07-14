using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dmart.Models;
using Dmart.Models.DBoperation;
namespace Dmart.Controllers
{
    public class HomeController : Controller
    {
        DmartRepository repo = null;
        public HomeController()
        {
            repo = new DmartRepository();
        }
        // GET: Home
        public ActionResult Index()
        {
            var result = new OrderDetail();
            result.or = repo.GetOrderDetails();
            return View(model);
        }
        public ActionResult Jqeury()
        {
            return View();  
        }
    }
}