using FundManager.Domain.Entities;
using System.Windows.Media;

namespace FundManager.Service.DTO
{
    public class StockDTO : Stock
    {
        public string TypeDescription => Type.ToString();

        public decimal TransactionCost { get; set; }
        public decimal StockWeight { get; set; }
        public bool Highlight { get; set; }
        
        public StockDTO(Stock stock, decimal transactionCost, decimal stockWeight, bool highlight)
        {
            Id = stock.Id;
            Name = stock.Name;
            Type = stock.Type;
            Price = stock.Price;
            Quantity = stock.Quantity;
            TransactionCost = transactionCost;
            StockWeight = stockWeight;
            Highlight = highlight;
        }
    }
}
