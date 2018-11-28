using System;
using System.ComponentModel;

namespace CommissionAPITestingTool.Models
{
    [Serializable]
    public class RemoteCommissionModel
    {
        [DisplayName("Tour Code")]
        public string TourCode { get; set; }

        [DisplayName("Is Commission Exists")]
        public bool CommissionExists { get; set; }

        [DisplayName("Apply By")]
        public string ApplyBy { get; set; }

        [DisplayName("Origin")]
        public string AppliedOrigin { get; set; }

        [DisplayName("Destination")]
        public string AppliedDestination { get; set; }

        [DisplayName("Commission Value")]
        public decimal CommissionValue { get; set; }
        public bool IsErrorOccured { get; set; }

        [DisplayName("Error")]
        public string ErrorMsg { get; set; }
    }
}