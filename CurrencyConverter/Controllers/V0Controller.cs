using CurrencyConverter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CurrencyConverter.Controllers
{
    public class V0Controller : ApiController
    {
        readonly string[] supportedCurrencies = { "USD", "GBP", "AUD", "EUR", "CAD", "SGD" };
        [HttpPost]

        public object rate(RateModel rates)
        {
            try
            {
                if (rates == null)
                {
                    rates.Amount = 1;
                    rates.CurrencyCode = "USD";
                }
                if (!supportedCurrencies.Contains(rates.CurrencyCode))
                {
                    return new
                    {
                        err = "Currency not supported",
                        returncode = -2
                    };
                }
                return GetResponse(rates.CurrencyCode, rates.Amount);
            }
            catch (Exception ex)
            {
                return new
                {
                    err = "Please try again later",
                    returncode = -1
                };
            }
        }

        [WebApiOutputCache(120, 60, false)]
        public object GetResponse(string CurrencyCode, double Amount)
        {
            using (ExchangeContainer db = new ExchangeContainer())
            {
                var currencyExRate = db.ExchangeRates.Where(x => x.CurrencyCode == CurrencyCode && x.IsActive).Select(r => new { r.Rate, r.UpdatedOn }).FirstOrDefault();
                double totalRates = Math.Round(Amount * currencyExRate.Rate, 2);
                double conversionRate = Math.Round(currencyExRate.Rate, 2);
                long ticks = currencyExRate.UpdatedOn.Ticks;
                return new
                {
                    SourceCurrency = CurrencyCode,
                    ConversionRate = conversionRate,
                    Amount = Amount,
                    Total = totalRates,
                    returncode = 1,
                    err = "success",
                    timestamp = ticks
                };
            }
        }
    }
}
