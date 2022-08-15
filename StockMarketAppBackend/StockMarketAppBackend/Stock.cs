namespace StockMarketAppBackend
{
    public class Stock
    {
        public string Name { get; set; }
        public float PriceNow { get; set; }

        public List<float> Diff { get; set; }

        public Stock(string name, float priceNow, List<float> diff)
        {
            Name = name;
            PriceNow = priceNow;
            Diff = diff;
        }

        public Stock()
        {

        }
    }
}
