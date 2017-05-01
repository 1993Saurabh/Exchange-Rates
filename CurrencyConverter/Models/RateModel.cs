using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CurrencyConverter.Models
{
    public class RateModel
    {
        public string CurrencyCode { get; set; }

        public float Amount { get; set; }
    }
}