using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Net;
using Newtonsoft.Json;
using airlineBooking.Models;
using airlineBooking.AppContants;
using airlineBooking.ViewModels;

namespace airlineBooking.Controllers
{
    /// <summary>
    /// Get Flight controller
    /// </summary>
    public class FlightController : Controller
    {

        #region Attributes
        #endregion Attributes

        #region Actions

        /// <summary>
        /// Display index view 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Search Flights
        /// </summary>
        /// <param name="flightModel"></param>
        /// <returns>null</returns>
        public ActionResult Flights(FlightModel flightModel)
        {
            var searchFlightViewModel = new SearchFlightViewModel();
            InsertFlightViewModel(RequestService(flightModel), searchFlightViewModel);

            //return View("SearchFlight");
            return null;
        }

        #endregion Actions

        #region Methods

        /// <summary>
        /// Gets session string in response data into web
        /// </summary>
        /// <param name="inputDataResponse"></param>
        /// <returns>session key</returns>
        public string SessionDataRequest(string inputDataResponse)
        {
            var sessionOfString = "";
            if (inputDataResponse.IndexOf("SessionKey", StringComparison.Ordinal) > 1 && inputDataResponse.IndexOf("OriginPlace", StringComparison.Ordinal) > 1)
            {
                for (var intdexOfDataRequest = inputDataResponse.IndexOf("SessionKey", StringComparison.Ordinal); intdexOfDataRequest < inputDataResponse.IndexOf("OriginPlace", StringComparison.Ordinal); intdexOfDataRequest++)
                {
                    sessionOfString += inputDataResponse[intdexOfDataRequest];
                }
            }

            var arrayAddSplit = new string[5];
            arrayAddSplit[0] = @":";
            arrayAddSplit[1] = @",";
            arrayAddSplit[2] = @"\";
            arrayAddSplit[3] = @"/";
            arrayAddSplit[4] = @"""";

            var sessionResultOutputData = sessionOfString.Split(arrayAddSplit, StringSplitOptions.RemoveEmptyEntries);

            if (sessionResultOutputData.Count() > 1)
            {
                return sessionResultOutputData[1].ToString(CultureInfo.InvariantCulture);
            }
            return string.Empty;
        }

        /// <summary>
        /// request web service
        /// </summary>
        /// <param name="flightModel"></param>
        /// <returns>dynamic result</returns>
        private dynamic RequestService(FlightModel flightModel)
        {
            try
            {
                var fromYMonth = String.Format("{0:yyMM}", flightModel.FromDate);
                var fromDate = String.Format("{0:dd}", flightModel.FromDate);
                var toYMonth = String.Format("{0:yyMM}", flightModel.ToDate);
                var toDate = String.Format("{0:dd}", flightModel.ToDate);
                var monthOfDateFlight = String.Format("{0:MM}", flightModel.FromDate);
                var yearOfDateFlight = String.Format("{0:yyyy}", flightModel.FromDate);

                string page = @"http://www.skyscanner.com.vn/flights/" + flightModel.FromCity + "/" + flightModel.ToCity +
                    "/" + fromYMonth + fromDate + "/" + toYMonth + toDate + "/cheapest-flights-from-vietnam-to-hanoi-trong-thang" + monthOfDateFlight + "-" + yearOfDateFlight  + ".html";

                var webClient = new WebClient();
                var outputData = webClient.DownloadString(page);
                var sessionKey = SessionDataRequest(outputData);

                string pageRequestWebService = @"http://www.skyscanner.com.vn/dataservices/routedate/v2.0/" + sessionKey;
                webClient.Headers.Add("Cookie", "scanner=usrplace:::" + Contants.Country + "&lang:::" + Contants.Locale + "&currency:::" + Contants.Currency +
                                                            "&fromCy:::" + flightModel.FromCountry + "&from:::" + flightModel.FromCity + 
                                                            "&toCy:::" + flightModel.ToCountry + "&to:::" + flightModel.ToCity +
                                                            "&adults:::1&children:::0&infants:::0&oym:::" + fromYMonth +
                                                            "&iym:::" + toYMonth + "&oday:::" + fromDate + "&iday:::" + toDate +
                                                            "&preferDirects:::false&cabinclass:::Economy&rtn:::true&dayviewtype:::1&charttype:::0&di:::false");

                var outputDataInXml = webClient.DownloadString(pageRequestWebService);

                return JsonConvert.DeserializeObject(outputDataInXml);
            }
            catch
            {
                throw new Exception();
            }
        }

        private void InsertFlightViewModel(dynamic result, SearchFlightViewModel searchFlightViewModel)
        {
        }

        #endregion Methods

    }
}
