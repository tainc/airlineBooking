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
            var result = RequestService(flightModel);

            var searchFlightViewModel = new SearchFlightViewModel();

            //searchFlightViewModel = InsertFlightViewModel(result, searchFlightViewModel);

            return View("SearchFlight",searchFlightViewModel);
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
            if (inputDataResponse.IndexOf("SessionKey") > 1 && inputDataResponse.IndexOf("OriginPlace") > 1) 
            {
                for (var intdexOfDataRequest = inputDataResponse.IndexOf("SessionKey"); intdexOfDataRequest < inputDataResponse.IndexOf("OriginPlace"); intdexOfDataRequest++)
                {
                    sessionOfString += inputDataResponse[intdexOfDataRequest];
                }
            }

            string[] arrayAddSplit = new string[5];
            arrayAddSplit[0] = @":";
            arrayAddSplit[1] = @",";
            arrayAddSplit[2] = @"\";
            arrayAddSplit[3] = @"/";
            arrayAddSplit[4] = @"""";

            var sessionResultOutputData = sessionOfString.Split(arrayAddSplit, StringSplitOptions.RemoveEmptyEntries);

            if (sessionResultOutputData.Count() > 1)
            {
                return sessionResultOutputData[1].ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// request web service
        /// </summary>
        /// <param name="flightModel"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns>dynamic result</returns>
        private dynamic RequestService(FlightModel flightModel)
        {
            String fromYMonth = String.Format("{0:yyMM}", flightModel.FromDate);
            String fromDate = String.Format("{0:dd}", flightModel.FromDate);
            String toYMonth = String.Format("{0:yyMM}", flightModel.ToDate);
            String toDate = String.Format("{0:dd}", flightModel.ToDate);

            string page = @"http://www.skyscanner.com.vn/flights/" + flightModel.FromCity + "/" + flightModel.ToCity +
                "/" + fromYMonth + "/" + toYMonth + "/cheapest-flights-from-vietnam-to-hanoi-trong-thang10-2013.html";

            WebClient webClient = new WebClient();
            String outputData = webClient.DownloadString(page);
            string sessionKey = SessionDataRequest(outputData);

            string pageRequestWebService = @"http://www.skyscanner.com.vn/dataservices/routedate/v2.0/" + sessionKey;
            webClient.Headers.Add("Cookie", "scanner=usrplace:::" + Contants.Country + "&lang:::" + Contants.Locale + "&currency:::" + Contants.Currency +
                                                        "&fromCy:::VN&from:::SGN&toCy:::VN&to:::HAN&adults:::1&children:::0&infants:::0&oym:::" + fromYMonth +
                                                        "&iym:::" + toYMonth + "&oday:::" + fromDate + "&iday:::" + toDate + 
                                                        "&preferDirects:::false&cabinclass:::Economy&rtn:::true&dayviewtype:::1&charttype:::0&di:::false");

            String outputDataInXml = webClient.DownloadString(pageRequestWebService);
            dynamic result = JsonConvert.DeserializeObject(outputDataInXml);

            return outputDataInXml;
        }

        /// <summary>
        /// insert models into searchFlightViewModel when web service response data
        /// </summary>
        /// <param name="result"></param>
        /// <param name="searchFlightViewModel"></param>
        /// <returns>SearchFlightViewModel searchFlightViewModel</returns>
        private SearchFlightViewModel InsertFlightViewModel(dynamic result, SearchFlightViewModel searchFlightViewModel)
        {

            //loop data of web service response, insert into CarriersModel and insert CarriersModel into searchFlightViewModel
            for (int indexCarriers = 0; indexCarriers < result.Carriers.Count; indexCarriers++)
            {
                var modelCarrier = new CarrierModel();
                if (String.IsNullOrEmpty(result.Carriers[indexCarriers].CarrierId.ToString()) == false)
                    modelCarrier.CarrierId = result.Carriers[indexCarriers].CarrierId;
                if (String.IsNullOrEmpty(result.Carriers[indexCarriers].Name.ToString()) == false)
                    modelCarrier.Name = result.Carriers[indexCarriers].Name;

                searchFlightViewModel.carriersModel.Add(modelCarrier);
            }


            //loop data of web service response, insert into QuotesModel and insert QuotesModel into searchFlightViewModel
            for (int indexQuote = 0; indexQuote < result.Quotes.Count; indexQuote++)
            {
                if (result.Quotes[indexQuote].OutboundLeg != null)
                    break;
                var modelQuote = new QuotesModel();

                //check data of web service response is not null
                if (String.IsNullOrEmpty(result.Quotes[indexQuote].QuoteId.ToString()) == false)
                    modelQuote.QuoteId = result.Quotes[indexQuote].QuoteId;
                if (String.IsNullOrEmpty(result.Quotes[indexQuote].MinPrice.ToString()) == false)
                    modelQuote.MinPrice = result.Quotes[indexQuote].MinPrice;
                if (String.IsNullOrEmpty(result.Quotes[indexQuote].Direct.ToString()) == false)
                    modelQuote.Direct = result.Quotes[indexQuote].Direct;
               
               
                if (String.IsNullOrEmpty(result.Quotes[indexQuote].OutboundLeg.OriginId.ToString()) == false)
                    modelQuote.Route.OriginId = result.Quotes[indexQuote].OutboundLeg.OriginId;
                if (String.IsNullOrEmpty(result.Quotes[indexQuote].OutboundLeg.DestinationId.ToString()) == false)
                    modelQuote.Route.DestinationId = result.Quotes[indexQuote].OutboundLeg.DestinationId;
                if (String.IsNullOrEmpty(result.Quotes[indexQuote].OutboundLeg.DepartureDate.ToString()) == false)
                    modelQuote.Route.DepartureDate = result.Quotes[indexQuote].OutboundLeg.DepartureDate;

                //loop data of QuotesModel, insert into CarrierIdsModel
                for (var indexCarrerId = 0; indexCarrerId < result.Quotes[indexQuote].OutboundLeg.CarrierIds.Count; indexCarrerId++)
                {
                    var modelCarrer = new CarrierModel();

                    modelCarrer.CarrierId = result.Quotes[indexQuote].OutboundLeg.CarrierIds[indexCarrerId];
                    modelCarrer.Name = this.requestCarrierNameFromId(modelCarrer.CarrierId,searchFlightViewModel);
                    modelQuote.Route.Carrier.Add(modelCarrer);
                }
                searchFlightViewModel.quotesModel.Add(modelQuote);
            }

            //loop data of web service response, insert into PlacesModel and insert PlacesModel into searchFlightViewModel
            for (int indexPlaces = 0; indexPlaces < result.Places.Count; indexPlaces++)
            {
                var modelPlace = new PlacesModel();
                if (String.IsNullOrEmpty(result.Places[indexPlaces].PlaceId.ToString()) == false)
                    modelPlace.PlaceId = result.Places[indexPlaces].PlaceId;
                if (String.IsNullOrEmpty(result.Places[indexPlaces].IataCode.ToString()) == false)
                    modelPlace.IataCode = result.Places[indexPlaces].IataCode;
                if (String.IsNullOrEmpty(result.Places[indexPlaces].Name.ToString()) == false)
                    modelPlace.Name = result.Places[indexPlaces].Name;
                if (String.IsNullOrEmpty(result.Places[indexPlaces].Type.ToString()) == false)
                    modelPlace.Type = result.Places[indexPlaces].Type;

                searchFlightViewModel.placesModel.Add(modelPlace);
            }

           

            return searchFlightViewModel;
        }

        private string requestCarrierNameFromId(int p, SearchFlightViewModel searchFlightViewModel)
        {
            for (var index = 0; index < searchFlightViewModel.carriersModel.Count; index++)
            {
                if (p == searchFlightViewModel.carriersModel[index].CarrierId) 
                {
                    return searchFlightViewModel.carriersModel[index].Name;
                }
            }
            return string.Empty;
        }

        #endregion Methods

    }
}
