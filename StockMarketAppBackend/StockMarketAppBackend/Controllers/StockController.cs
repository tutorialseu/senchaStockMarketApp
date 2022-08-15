using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Collections;
using System.Net;
using System.Net.Http;

using Newtonsoft.Json.Linq;


namespace StockMarketAppBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        static public List<Stock> stocks = new List<Stock>()
        {

        };

        [HttpGet]
        public IEnumerable<Stock> Get()
        {
            // Grab the latest stock information

            foreach (var stock in stocks)
            {
                float price = GetPrice(stock.Name);

                if (price < 0f)
                    price = 0;

                stock.PriceNow = price;
            }

            return stocks;
        }

        [HttpGet("{id}")]
        public ActionResult<Stock> Find(string id)
        {
            int index = stocks.FindIndex(x => x.Name == id);
            if (index == -1)
                return NotFound();

            return Ok(stocks[index]);
        }

        private void GetHistData(Stock stock)
        {
            string token = "";

            string url = String.Format(
                "https://cloud.iexapis.com/v1/stock/{0}/chart/1m?token={1}"
                , stock.Name
                , token);

            var hist = GetData(url);
            JArray jsonHist = JArray.Parse(hist);

            foreach(JObject item in jsonHist)
            {
                float tmp = (float)item["open"];
                stock.Diff.Add(tmp);
                
            }
        }

        [HttpPost]
        public IActionResult Create(Param param)
        {
            int index = stocks.FindIndex(x => x.Name == param.nameInp);
            if (index > -1)
                return BadRequest();

            float price = GetPrice(param.nameInp);
            if (price < 0)
                return BadRequest();

            // Grab some historical price data for this stock
            Stock newStock = new Stock();
            newStock.Diff = new List<float>();
            newStock.Name = param.nameInp;
            newStock.PriceNow = price;
            GetHistData(newStock);

            stocks.Add(newStock);

            return Ok();            
        }


        static private string GetData(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            try
            {
                using var webResponse = request.GetResponse();
                using var webStream = webResponse.GetResponseStream();

                using var reader = new StreamReader(webStream);
                var data = reader.ReadToEnd();
                return data;
            }
            catch (Exception e)
            {
                return "";
            }
        }

        private float GetPrice(string param)
        {
            string token = "";

            string url = String.Format(
                "https://cloud.iexapis.com/v1/stock/{0}/quote?token={1}"
                , param
                , token);

            var res = GetData(url);

            if (res == null)
                return -1;

            JObject json = JObject.Parse(res);

            if (json["latestPrice"] == null)
                return -1;

            float price = (float)json["latestPrice"];
            return price;

        }
    }

    public class Param
    {
        public string nameInp { get; set; }
    }
}
