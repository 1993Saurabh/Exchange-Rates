using HtmlAgilityPack;
using Quartz;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurrencyConverter
{
    public class ExchangeRateJob : IJob
    {
        readonly string[] supportedCurrencies = { "USD", "GBP", "AUD", "EUR", "CAD", "SGD" };
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                float amount = 1;
                for (int i = 0; i < supportedCurrencies.Length; i++)
                {
                    float rate = GetExchangeRates(supportedCurrencies[i], amount);
                    StoreRates(supportedCurrencies[i], rate);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void StoreRates(string currencyCode, float rate)
        {
            try
            {
                using (ExchangeContainer db = new ExchangeContainer())
                {
                    ExchangeRate rates = db.ExchangeRates.Where(x => x.CurrencyCode.Equals(currencyCode) && x.IsActive).FirstOrDefault();
                    if (rates == null)
                    {
                        rates = new ExchangeRate();
                        rates.CurrencyCode = currencyCode;
                        rates.IsActive = true;
                        rates.Rate = rate;
                        rates.UpdatedOn = DateTime.UtcNow;
                        db.ExchangeRates.Add(rates);
                    }
                    else
                    {
                        rates.CurrencyCode = currencyCode;
                        rates.IsActive = true;
                        rates.Rate = rate;
                        rates.UpdatedOn = DateTime.UtcNow;
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private float GetExchangeRates(string currency, float amount)
        {
            string BASE_URL = "https://www.google.com/finance/";
            string requestUrl = string.Format(BASE_URL);
            RestClient client = new RestClient(requestUrl);
            RestRequest req = new RestRequest("converter", Method.GET);
            req.AddParameter("a", amount);
            req.AddParameter("from", currency);
            req.AddParameter("to", "INR");
            IRestResponse response = client.Execute(req);
            var content = response.Content;
            string rate = GetRateExchangeValue(content);
            return (float.Parse(rate.Split(' ')[0].ToString()));
        }
        private string GetRateExchangeValue(string htmlString)
        {
            try
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(htmlString);
                var result = doc.DocumentNode.SelectNodes("//span[(@class='bld')]").FirstOrDefault().InnerText;
                return result;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}