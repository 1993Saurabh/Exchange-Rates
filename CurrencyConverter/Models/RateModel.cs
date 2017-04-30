using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CurrencyConverter.Models
{
    public class RateModel
    {
        [DefaultValue("USD")]
        public string CurrencyCode { get; set; }

        [DefaultValue(1)]
        public float Amount { get; set; }
    }
}