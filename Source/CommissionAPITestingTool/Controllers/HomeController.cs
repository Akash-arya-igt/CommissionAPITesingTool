using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using CommissionAPITestingTool.Models;
using System.Web.Script.Serialization;

namespace CommissionAPITestingTool.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string sessionkey)
        {
            if (string.IsNullOrEmpty(sessionkey))
                return RedirectToAction("Index", "Login");

            return View();
        }

        // POST: CommissionTesting/GetCommission
        [HttpPost]
        public ActionResult GetCommission(string sessionkey, CommissionRequest commDetailReq)
        {
            RemoteCommissionModel comVal = new RemoteCommissionModel();
            try
            {
                List<FlightInfo> airlineInfos = new List<FlightInfo>();
                if (commDetailReq.AirlineCode != null && !string.IsNullOrEmpty(commDetailReq.AirlineCode))
                    airlineInfos = commDetailReq.AirlineCode.Split(',').Where(x => x != null && !string.IsNullOrEmpty(x))
                        .Select(x => new FlightInfo()
                        {
                            AirlineCode = (x.Substring(0, x.IndexOf('-')).ToUpper()),
                            FlightNumber = Convert.ToInt16(x.Substring(x.IndexOf('-') + 1))
                        }).ToList();

                string URLvalue = System.Configuration.ConfigurationManager.AppSettings["CommAPIUrl"];

                RemoteCommissionRequestModel objCommDetailReq = new RemoteCommissionRequestModel()
                {
                    TravelClassName = commDetailReq.TravelClass != null && !string.IsNullOrEmpty(commDetailReq.TravelClass) ? commDetailReq.TravelClass.Split(',').ToList() : new List<string>(),
                    BookingClass = commDetailReq.BookingClass != null && !string.IsNullOrEmpty(commDetailReq.BookingClass) ? commDetailReq.BookingClass.Split(',').ToList() : new List<string>(),
                    LstFareBasis = commDetailReq.FareBasis != null && !string.IsNullOrEmpty(commDetailReq.FareBasis) ? commDetailReq.FareBasis.Split(',').ToList() : new List<string>(),
                    CarrierCode = commDetailReq.carrierCode,
                    Source = commDetailReq.source,
                    Destination = commDetailReq.destination,
                    PaxType = commDetailReq.strPaxType,
                    TicketingDate = commDetailReq.dtTicketingDate.ToString("dd/MM/yyyy"),
                    StartTravelDate = commDetailReq.dtStartTravelDate.ToString("dd/MM/yyyy"),
                    EndTravelDate = commDetailReq.dtEndTravelDate.ToString("dd/MM/yyyy"),
                    PCC = commDetailReq.pcc,
                    IsPackageBooking = commDetailReq.isPackageBooking,
                    IsPrivateBooking = commDetailReq.isPrivateBooking,
                    LstAirline_FltNum = airlineInfos
                };

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(URLvalue);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Session-Key", sessionkey);

                    var resp = client.PostAsync(URLvalue + "CommissionValue",
                                                 new StringContent(new JavaScriptSerializer().Serialize(objCommDetailReq),
                                                                   Encoding.UTF8, "application/json")).Result;

                    if (resp.IsSuccessStatusCode)
                    {
                        var shoppingResp = resp.Content.ReadAsAsync<RemoteCommissionModel>();
                        comVal = shoppingResp.Result;
                        comVal.IsErrorOccured = false;
                    }
                    else
                    {
                        var errorMsg = resp.ReasonPhrase;
                        comVal.IsErrorOccured = true;

                        if (errorMsg.StartsWith("Session key"))
                            comVal.ErrorMsg = "Session key is invalid. Please login again.";
                        else
                            comVal.ErrorMsg = errorMsg;
                    }
                }

                return PartialView("ShowCommissionResult", comVal);
            }
            catch (Exception ex)
            {
                comVal.IsErrorOccured = true;
                comVal.ErrorMsg = ex.Message;
                return PartialView("ShowCommissionResult", comVal);
            }
        }

        [HttpGet]
        public PartialViewResult ShowCommissionResult(RemoteCommissionModel CommObj)
        {
            return PartialView(CommObj);
        }
    }
}