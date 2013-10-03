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
            String fromDate = String.Format("{0:yyyy-MM-dd}", flightModel.FromDate);
            String toDate = String.Format("{0:yyyy-MM-dd}", flightModel.ToDate);

            var result = RequestService(flightModel, fromDate, toDate);
            var searchFlightViewModel = new SearchFlightViewModel();

            searchFlightViewModel = InsertFlightViewModel(result, searchFlightViewModel);

            return null;
        }

        #endregion Actions

        #region Methods

        /// <summary>
        /// request web service
        /// </summary>
        /// <param name="flightModel"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns>dynamic result</returns>
        private dynamic RequestService(FlightModel flightModel, String fromDate, String toDate)
        {
            string page = @"http://partners.api.skyscanner.net/apiservices/browsequotes/v1.0/" + Contants.Country + "/" + Contants.Currency +
                                            "/" + Contants.Locale + "/" + flightModel.FromCity + "/" + flightModel.ToCity + "/" + fromDate +
                                            "/" + toDate + "?apiKey=" + Contants.APIWebservice;

            WebClient webClient = new WebClient();
            String outputData = webClient.DownloadString(page);
            dynamic result = JsonConvert.DeserializeObject(outputData);

            return result;
        }

        /// <summary>
        /// insert models into searchFlightViewModel when web service response data
        /// </summary>
        /// <param name="result"></param>
        /// <param name="searchFlightViewModel"></param>
        /// <returns>SearchFlightViewModel searchFlightViewModel</returns>
        private SearchFlightViewModel InsertFlightViewModel(dynamic result, SearchFlightViewModel searchFlightViewModel)
        {
            //loop data of web service response, insert into QuotesModel and insert QuotesModel into searchFlightViewModel
            for (int indexQuote = 0; indexQuote < result.Quotes.Count; indexQuote++)
            {
                var modelQuote = new QuotesModel();

                //check data of web service response is not null
                if (String.IsNullOrEmpty(result.Quotes[indexQuote].QuoteId.ToString()) == false)
                    modelQuote.QuoteId = result.Quotes[indexQuote].QuoteId;
                if (String.IsNullOrEmpty(result.Quotes[indexQuote].MinPrice.ToString()) == false)
                    modelQuote.MinPrice = result.Quotes[indexQuote].MinPrice;
                if (String.IsNullOrEmpty(result.Quotes[indexQuote].Direct.ToString()) == false)
                    modelQuote.Direct = result.Quotes[indexQuote].Direct;
                if (String.IsNullOrEmpty(result.Quotes[indexQuote].OutboundLeg.OriginId.ToString()) == false)
                    modelQuote.OutboundLeg.OriginId = result.Quotes[indexQuote].OutboundLeg.OriginId;
                if (String.IsNullOrEmpty(result.Quotes[indexQuote].OutboundLeg.DestinationId.ToString()) == false)
                    modelQuote.OutboundLeg.DestinationId = result.Quotes[indexQuote].OutboundLeg.DestinationId;
                if (String.IsNullOrEmpty(result.Quotes[indexQuote].OutboundLeg.DepartureDate.ToString()) == false)
                    modelQuote.OutboundLeg.DepartureDate = result.Quotes[indexQuote].OutboundLeg.DepartureDate;

                //loop data of QuotesModel, insert into CarrierIdsModel
                for (var indexCarrerId = 0; indexCarrerId < result.Quotes[indexQuote].OutboundLeg.CarrierIds.Count; indexCarrerId++)
                {
                    var modelCarrerIds = new CarrierIdsModel();
                    
                    modelCarrerIds.CarrierId = result.Quotes[indexQuote].OutboundLeg.CarrierIds[indexCarrerId];
                    modelQuote.OutboundLeg.CarrerIds.Add(modelCarrerIds);
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

            //loop data of web service response, insert into CarriersModel and insert CarriersModel into searchFlightViewModel
            for (int indexCarriers = 0; indexCarriers < result.Carriers.Count; indexCarriers++)
            {
                var modelCarrier = new CarriersModel();
                if (String.IsNullOrEmpty(result.Carriers[indexCarriers].CarrierId.ToString()) == false)
                    modelCarrier.CarrierId = result.Carriers[indexCarriers].CarrierId;
                if (String.IsNullOrEmpty(result.Carriers[indexCarriers].Name.ToString()) == false)
                    modelCarrier.Name = result.Carriers[indexCarriers].Name;

                searchFlightViewModel.carriersModel.Add(modelCarrier);
            }

            return searchFlightViewModel;
        }

        #endregion Methods

    }
}
