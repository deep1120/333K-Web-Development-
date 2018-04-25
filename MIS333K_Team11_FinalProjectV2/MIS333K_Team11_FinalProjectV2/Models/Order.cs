﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MIS333K_Team11_FinalProjectV2.Models 
{
    public enum OrderStatus
    {
        Pending, Completed, Cancelled
    }

    public class Order
    {
        private const Decimal SALES_TAX = 0.0825m; 

        public Int32 OrderID { get; set; }

        [Display(Name = "Order Number")] //Transaction number? do we need this?
        public Int32 OrderNumber { get; set; }

        [Display(Name = "Order Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Order Notes")] //this may not be needed
        public String OrderNotes { get; set; }

        [Display(Name = "Card Number")]
        public String CardNumber { get; set; }

        public Decimal Total { get; set; } //this isn't right at all, just put this here so code would be happy

        //[Required(ErrorMessage = "Order status is required")]
        [Display(Name = "Order Status")]
        public OrderStatus Orderstatus { get; set; }

        [Display(Name = "Order Subtotal")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal OrderSubtotal
        {
            get { return Tickets.Sum(od => od.TotalFees); }
        }

        [Display(Name = "Sales Tax (%)")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal SalesTax
        {
            get { return OrderSubtotal * SALES_TAX; }
        }
        [Display(Name = "Order Total")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal OrderTotal
        {
            get { return OrderSubtotal + SalesTax; }
        }

        [Required(ErrorMessage = "Gift is required.")]
        [Display(Name = "Is this order a gift?")]
        public Boolean Gift { get; set; }


        public virtual List<Ticket> Tickets { get; set; }
        public virtual AppUser Purchased { get; set; }
        public virtual AppUser Gifted { get; set; }

        public Order()
        {
            if (Tickets == null)
            {
                Tickets = new List<Ticket>();
            }
        }
        
    }
}
