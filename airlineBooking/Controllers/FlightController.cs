using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace airlineBooking.Controllers
{
    public class FlightController : Controller
    {
        //
        // GET: /Flight/

        public ActionResult Index()
        {
            string page = @"http://partners.api.skyscanner.net/apiservices/browsedates/v1.0/VN/VND/vi-VN/SGN/HAN/anytime/anytime?apiKey=7f75b175-ca41-4613-8e84-097d89cbb01b";
            WebClient webClient = new WebClient();
            String outputData = webClient.DownloadString(page);
            dynamic result = JsonConvert.DeserializeObject(outputData);



            ViewBag.ShowMessage = result.Dates.OutboundDates[0].Price;
            
            
            return View();
        }

    }
}
