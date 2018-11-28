using System.Collections.Generic;

namespace CommissionAPITestingTool.Models
{
    public class RemoteCommissionRequestModel
    {
        public string CarrierCode { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public List<string> TravelClassName { get; set; }
        public List<string> LstFareBasis { get; set; }
        public List<string> BookingClass { get; set; }
        public string PaxType { get; set; }
        public string TicketingDate { get; set; }
        public string StartTravelDate { get; set; }
        public string EndTravelDate { get; set; }
        public string PCC { get; set; }
        public bool IsPackageBooking { get; set; }
        public bool IsPrivateBooking { get; set; }
        public List<FlightInfo> LstAirline_FltNum { get; set; }
    }
}