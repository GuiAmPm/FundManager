namespace FundManager.Domain.Entities
{
    public class Stock
    {
        public long Id { get; set; }
        
        public StockType Type { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal MarketValue => Price * Quantity;
    }
}
