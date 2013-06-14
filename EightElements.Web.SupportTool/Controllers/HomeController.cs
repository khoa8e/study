using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EightElements.Web.SupportTool.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
            //return new RedirectResult("/Transactions/Search");
        }

        public ActionResult Error()
        {
            return View();
        }

    }
}
