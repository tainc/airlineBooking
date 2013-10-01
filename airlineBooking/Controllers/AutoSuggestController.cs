using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace airlineBooking.Controllers
{
    public class AutoSuggestController : Controller
    {
        //
        // GET: /AutoSuggest/

        public ActionResult Index(string query)
        {
            string page = @"http://partners.api.skyscanner.net/apiservices/autosuggest/v1.0/VN/VND/vi-VN/?query=" + query + "&apikey=7f75b175-ca41-4613-8e84-097d89cbb01b";
            WebClient webClient = new WebClient();
            
            String outputData =webClient.DownloadString(page);
            byte[] bytes = Encoding.Default.GetBytes(outputData);
            outputData = Encoding.UTF8.GetString(bytes);
            dynamic result = JsonConvert.DeserializeObject(outputData);
            this.Response.Headers.Add("Content-type", "application/json; charset=utf-8");
            this.Response.Write(outputData); this.Response.End();
            return null;
        }

    }
}
