using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CommissionAPITestingTool.Models
{
    public class CommissionRequest
    {
        [DisplayName("Carrier Code")]
        public string carrierCode { get; set; }

        [DisplayName("Source")]
        public string source { get; set; }

        [DisplayName("Destination")]
        public string destination { get; set; }

        [DisplayName("Pax Type")]
        public string strPaxType { get; set; }

        [DisplayName("Ticketing Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime dtTicketingDate { get; set; }

        [DisplayName("Travel Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime dtStartTravelDate { get; set; }

        [DisplayName("Travel End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime dtEndTravelDate { get; set; }

        [DisplayName("PCC")]
        public string pcc { get; set; }

        [DisplayName("Package Booking")]
        public bool isPackageBooking { get; set; }

        [DisplayName("Private Booking")]
        public bool isPrivateBooking { get; set; }

        [DisplayName("Booking Class List")]
        public string BookingClass { get; set; }

        [DisplayName("TravelClass List")]
        public string TravelClass { get; set; }

        [DisplayName("FareBasis List")]
        public string FareBasis { get; set; }

        [DisplayName("Airline-Flight# List")]
        public string AirlineCode { get; set; }
    }
}