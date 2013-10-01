using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using airlineBooking.Models;

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

        public ActionResult Flights(FlightModel model) {
            String fromDate = String.Format("{0:yyyy-MM-dd}", model.fromDate);
            String toDate = String.Format("{0:yyyy-MM-dd}", model.toDate);

            string page = @"http://partners.api.skyscanner.net/apiservices/browsequotes/v1.0/VN/VND/vi-VN/" + model.fromCity + "/" + model.toCity + "/" + fromDate + "/" + toDate + "?apiKey=7f75b175-ca41-4613-8e84-097d89cbb01b";
            WebClient webClient = new WebClient();
            String outputData = webClient.DownloadString(page);
            dynamic result = JsonConvert.DeserializeObject(outputData);
            var index = result.Quotes;
            var modelQuotes = new QuotesModel();
            for (int indexQuote = 0; indexQuote < result.Quotes.Count; indexQuote++) 
            { 
                modelQuotes.QuoteId = result.Quotes[indexQuote].QuoteId;
                modelQuotes.MinPrice = result.Quotes[indexQuote].MinPrice;
                modelQuotes.Direct = result.Quotes[indexQuote].Direct;
                modelQuotes.OutboundLeg.OriginId = result.Quotes[indexQuote].OutboundLeg.OriginId;
                modelQuotes.OutboundLeg.DestinationId = result.Quotes[indexQuote].OutboundLeg.DestinationId;
                modelQuotes.OutboundLeg.DepartureDate = result.Quotes[indexQuote].OutboundLeg.DepartureDate;
                for (var indexCarrerId = 0; indexCarrerId < result.Quotes[indexQuote].OutboundLeg.CarrierIds.Count; indexCarrerId++) 
                {
                    modelQuotes.OutboundLeg.CarrerIds.Add(result.Quotes[indexQuote].OutboundLeg.CarrierIds[indexCarrerId]);
                }
            }

            return null;
        }

    }
}
