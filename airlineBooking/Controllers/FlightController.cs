using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace airlineBooking.Controllers
{
    public class FlightController : Controller
    {
        //
        // GET: /Flight/

        public ActionResult Index()
        {
            return View();
        }

    }
}
