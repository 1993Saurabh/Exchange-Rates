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
                if (ModelState.IsValid)
                {
                    using (ExchangeContainer db = new ExchangeContainer())
                    {
                        var currencyExRate = db.ExchangeRates.Where(x => x.CurrencyCode == rates.CurrencyCode && x.IsActive).Select(r => new { r.Rate, r.UpdatedOn }).FirstOrDefault();
                        double totalRates = Math.Round(rates.Amount * currencyExRate.Rate, 2);
                        double conversionRate = Math.Round(currencyExRate.Rate, 2);
                        long ticks = currencyExRate.UpdatedOn.Ticks;
                        return new
                        {
                            SourceCurrency = rates.CurrencyCode,
                            ConversionRate = conversionRate,
                            Amount = rates.Amount,
                            Total = totalRates,
                            returncode = 1,
                            err = "success",
                            timestamp = ticks
                        };
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return new
                {
                    err = "error",
                    returncode = -1
                };
            }
        }

    }
}
