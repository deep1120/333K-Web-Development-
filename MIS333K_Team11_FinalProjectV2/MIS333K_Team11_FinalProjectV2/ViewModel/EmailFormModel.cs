using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MIS333K_Team11_FinalProjectV2.ViewModel
{
    public class EmailFormModel
    {
        public String Subject { get; set; }
        public String Sender { get; set; }
        public String From { get; set; }
        public String To { get; set; }
        public String Body { get; set; }
    }
}