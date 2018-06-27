using FundManager.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FundManager.Tests.Domain
{
    [TestClass]
    public class StockTests
    {
        [TestMethod]
        public void CanCreateStock()
        {
            var stock = new Stock();
        }

        [DataTestMethod]
        [DataRow(1.00, 1, 1.00)]
        [DataRow(12.00, 2, 24.00)]
        [DataRow(10.00, 10, 100.00)]
        public void TestMarketValue(double price, int quantity, double expectedMarketValue)
        {
            Stock stock = new Stock
            {
                Price = (decimal)price,
                Quantity = quantity
            };

            Assert.AreEqual((decimal)expectedMarketValue, stock.MarketValue);
        }
    }
}
