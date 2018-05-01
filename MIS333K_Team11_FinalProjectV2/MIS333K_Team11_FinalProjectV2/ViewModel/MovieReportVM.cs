using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MIS333K_Team11_FinalProjectV2.ViewModel
{
    public class MovieReportVM
    {
        public string MovieTitle { get; set; }
        public int MovieID { get; set; }
        public int NumberOfPurchase { get; set; }
        public decimal Revenue { get; set; }
    }
}