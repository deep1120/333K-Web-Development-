using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using MIS333K_Team11_FinalProjectV2.Models;

namespace MIS333K_Team11_FinalProjectV2.ViewModel
{
    public class CheckoutViewModel
    {
        private String _CardNumber;

        public string SelectedCardNumber { get; set; }

        public String CardOption { get; set; }

        public String AppUserId { get; set; }

        [Display(Name = "Type of Card")]
        public Card.CardType Type { get; set; }


        [Display(Name = "Card Number")]
        [RegularExpression(@"^.{5,}$", ErrorMessage = "Invalid card number")]
        public string CardNumber
        {
            get { return (string.IsNullOrEmpty(_CardNumber)) ? string.Empty : string.Concat(string.Empty.PadLeft(_CardNumber.Length - 4, '*'), _CardNumber.Substring(_CardNumber.Length - 4)); }
            set { _CardNumber = value; }

        }



        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!Regex.IsMatch(_CardNumber, @"^[0-9]{15,16}$"))
            {
                yield return new ValidationResult
                 ("Card number is invalid", new[] { "CardNumber" });
            }

            if (_CardNumber.Length == 15 && Type != Card.CardType.AmericanExpress)
            {
                yield return new ValidationResult
                    ("Card number does not match card type.");
            }
            if (_CardNumber.Substring(0, 2) != "54" && Type == Card.CardType.Mastercard)
            {
                yield return new ValidationResult
                    ("Card number does not match card type.");
            }
            if (_CardNumber.Substring(0, 1) != "4" && Type == Card.CardType.Visa)
            {
                yield return new ValidationResult
                    ("Card number does not match card type.");
            }
            if (_CardNumber.Substring(0, 1) != "6" && Type == Card.CardType.Discover)
            {
                yield return new ValidationResult
                    ("Card number does not match card type.");
            }

        }

        [Display(Name = "Expiration Date")]
        public string ExpDate { get; set; }

        [RegularExpression(@"^\d{3}$", ErrorMessage = "Enter a valid CVV (3 digit numeric data)")]
        public string CVV { get; set; }

        public string Subtotal { get; set; }

        public string Tax { get; set; }

        public string Total { get; set; }

        [Display(Name = "Is this a gift?")]
        public bool IsGift { get; set; }

        [Display(Name = "Gift Email")]
        public string GiftEmail { get; set; }

        public List<SelectListItem> ListOfCards { get; set; }
    }
}