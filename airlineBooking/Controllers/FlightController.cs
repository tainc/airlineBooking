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

            return View("SearchFlight", searchFlightViewModel);
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
                    "/" + fromYMonth + fromDate + "/" + toYMonth + toDate + "/cheapest-flights-from-vietnam-to-hanoi-trong-thang" + monthOfDateFlight + "-" + yearOfDateFlight + ".html";

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

        /// <summary>
        /// Sets data response of web service into SearchFlightViewModel
        /// </summary>
        /// <param name="result"></param>
        /// <param name="searchFlightViewModel"></param>
        private void InsertFlightViewModel(dynamic result, SearchFlightViewModel searchFlightViewModel)
        {
            for (var indexOfQuoteRequests = 0;
                 indexOfQuoteRequests < result.QuoteRequests.Count;
                 indexOfQuoteRequests++)
            {
                var quoteRequestModel = new QuoteRequestModel
                    {
                        Id = result.QuoteRequests[indexOfQuoteRequests].Id,
                        AngentId = result.QuoteRequests[indexOfQuoteRequests].AgentId,
                        HasResults = result.QuoteRequests[indexOfQuoteRequests].HasResults,
                        JacquardPoolId = result.QuoteRequests[indexOfQuoteRequests].JacquardPoolId
                    };
                searchFlightViewModel.QuoteRequestsModelList.Add(quoteRequestModel);
            }

            for (var indexOfQuotes = 0; indexOfQuotes < result.Quotes.Count; indexOfQuotes++)
            {
                var quoteModel = new QuotesModel
                    {
                        Id = result.Quotes[indexOfQuotes].Id,
                        QuoteRequestId = result.Quotes[indexOfQuotes].QuoteRequestId,
                        RequestDateTime = result.Quotes[indexOfQuotes].RequestDateTime,
                        Age = result.Quotes[indexOfQuotes].Age,
                        Price = result.Quotes[indexOfQuotes].Price,
                        IsReturn = result.Quotes[indexOfQuotes].IsReturn
                    };
                searchFlightViewModel.QuotesModelList.Add(quoteModel);
            }

            for (var indexOfOutboundItineraryLegs = 0;
                 indexOfOutboundItineraryLegs < result.OutboundItineraryLegs.Count;
                 indexOfOutboundItineraryLegs++)
            {
                var outboundItineraryLegModel = new OutboundItineraryLegModel
                    {
                        Id = result.OutboundItineraryLegs[indexOfOutboundItineraryLegs].Id,
                        OriginStation = result.OutboundItineraryLegs[indexOfOutboundItineraryLegs].OriginStation,
                        DestinationStation = result.OutboundItineraryLegs[indexOfOutboundItineraryLegs].DestinationStation,
                        DepartureDateTime = result.OutboundItineraryLegs[indexOfOutboundItineraryLegs].DepartureDateTime,
                        ArrivalDateTime = result.OutboundItineraryLegs[indexOfOutboundItineraryLegs].ArrivalDateTime,
                        Duration = result.OutboundItineraryLegs[indexOfOutboundItineraryLegs].Duration,
                        ItineraryLegType = result.OutboundItineraryLegs[indexOfOutboundItineraryLegs].ItineraryLegType
                    };
                InsertMarketingCarrierIdsInOutboundItineraryLegs(indexOfOutboundItineraryLegs, result,
                                                                 outboundItineraryLegModel);

                InsertOperatingCarrierIdsInOutboundItineraryLegs(indexOfOutboundItineraryLegs, result,
                                                                 outboundItineraryLegModel);

                InsertPricingOptionsInOutboundItineraryLegs(indexOfOutboundItineraryLegs, result,
                                                                 outboundItineraryLegModel);

                searchFlightViewModel.OutboundItineraryLegsesModelList.Add(outboundItineraryLegModel);
            }

        }

        /// <summary>
        /// Sets field MarketingCarrierIds of OutboundItineraryLegs 
        /// </summary>
        /// <param name="indexOfOutboundItineraryLegs"></param>
        /// <param name="result"></param>
        /// <param name="outboundItineraryLegModel"></param>
        private void InsertMarketingCarrierIdsInOutboundItineraryLegs(int indexOfOutboundItineraryLegs, dynamic result,
                                                                      OutboundItineraryLegModel
                                                                          outboundItineraryLegModel)
        {
            for (var indexOfMarketingCarrierIds = 0;
                    indexOfMarketingCarrierIds <
                    result.OutboundItineraryLegs[indexOfOutboundItineraryLegs].MarketingCarrierIds.Count;
                    indexOfMarketingCarrierIds++)
            {
                outboundItineraryLegModel.MarketingCarrierIds.Add(result.OutboundItineraryLegs[indexOfOutboundItineraryLegs].MarketingCarrierIds[indexOfMarketingCarrierIds].ToString());
            }
        }

        /// <summary>
        /// Sets field OperatingCarrierIds of OutboundItineraryLegs 
        /// </summary>
        /// <param name="indexOfOutboundItineraryLegs"></param>
        /// <param name="result"></param>
        /// <param name="outboundItineraryLegModel"></param>
        private void InsertOperatingCarrierIdsInOutboundItineraryLegs(int indexOfOutboundItineraryLegs, dynamic result,
                                                                      OutboundItineraryLegModel
                                                                          outboundItineraryLegModel)
        {
            for (var indexOfOperatingCarrierIds = 0;
                   indexOfOperatingCarrierIds <
                   result.OutboundItineraryLegs[indexOfOutboundItineraryLegs].OperatingCarrierIds.Count;
                   indexOfOperatingCarrierIds++)
            {
                outboundItineraryLegModel.OperatingCarrierIds.Add(result.OutboundItineraryLegs[indexOfOutboundItineraryLegs].OperatingCarrierIds[indexOfOperatingCarrierIds].ToString());
            }
        }

        /// <summary>
        /// Sets field PricingOptions of OutboundItineraryLegs
        /// </summary>
        /// <param name="indexOfOutboundItineraryLegs"></param>
        /// <param name="result"></param>
        /// <param name="outboundItineraryLegModel"></param>
        private void InsertPricingOptionsInOutboundItineraryLegs(int indexOfOutboundItineraryLegs, dynamic result,
                                                                      OutboundItineraryLegModel
                                                                          outboundItineraryLegModel)
        {
            for (var indexOfPricingOptions = 0;
                   indexOfPricingOptions <
                   result.OutboundItineraryLegs[indexOfOutboundItineraryLegs].PricingOptions.Count;
                   indexOfPricingOptions++)
            {
                var pricingOptionModelList = new PricingOptionModel
                    {
                        IsConstrained = result.OutboundItineraryLegs[indexOfOutboundItineraryLegs].PricingOptions[indexOfPricingOptions].IsConstrained
                    };
                InsertQuoteIdsInPricingOptions(indexOfOutboundItineraryLegs, result, pricingOptionModelList,
                                               indexOfPricingOptions);
                
                outboundItineraryLegModel.PricingOptionModelList.Add(pricingOptionModelList);
            }
        }

        private void InsertQuoteIdsInPricingOptions(int indexOfOutboundItineraryLegs, dynamic result,
                                                    PricingOptionModel
                                                        pricingOptionModelList, int indexOfPricingOptions)
        {
            for (var indexOfQuoteIds = 0;
                     indexOfQuoteIds <
                     result.OutboundItineraryLegs[indexOfOutboundItineraryLegs].PricingOptions[indexOfPricingOptions]
                         .QuoteIds.Count;
                     indexOfQuoteIds++)
            {
                pricingOptionModelList.QuoteIds.Add(result.OutboundItineraryLegs[indexOfOutboundItineraryLegs].PricingOptions[indexOfPricingOptions].QuoteIds[indexOfQuoteIds].ToString());
            }
        }

        #endregion Methods

    }
}
