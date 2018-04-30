using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MIS333K_Team11_FinalProjectV2.Models;

namespace MIS333K_Team11_FinalProjectV2.ViewModels
{
    public class ItemRatingVM
    {
        public Movie Movie { get; set; }
        public double Rating { get; set; }
    }
}