using FundManager.Domain.Entities;
using FundManager.Repository;
using FundManager.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FundManager.Tests.Service
{
    [TestClass]
    public class StockServiceTests
    {
        [TestMethod]
        public void CanCreateService()
        {
            var service = new StockService();
        }

        [TestMethod]
        public void CanCount()
        {
            var service = new StockService();

            Assert.AreEqual(0, service.Count());
        }

        [TestMethod]
        public void CanCountWithPredicate()
        {
            var service = new StockService();
            service.Count(x => 1 == 0);

            Assert.AreEqual(0, service.Count());
        }

        [TestMethod]
        public void CanCreateStock()
        {
            var service = new StockService();
            service.CreateNewStock(StockType.Bond, 1, 1);

            Assert.AreEqual(1, service.Count());
        }

        [TestMethod]
        public void CanGetDTO()
        {
            var service = new StockService();
            service.GetDTO(1);
        }


        [TestMethod]
        public void CanGetDTOAfterCreateNew()
        {
            var service = new StockService();
            service.CreateNewStock(StockType.Bond, 1, 1);
            var dto = service.GetDTO(1);

            Assert.IsNotNull(dto);
        }

        [TestMethod]
        public void CanList()
        {
            var service = new StockService();
            service.ListDTO();
        }

        [TestMethod]
        public void CanListAfterInsert()
        {
            var service = new StockService();

            service.CreateNewStock(StockType.Bond, 1, 1);

            var list = service.ListDTO();

            Assert.AreEqual(1, list.Count);
        }
        
        private void TestInputForTotalsTest(StockService service)
        {
            service.CreateNewStock(StockType.Bond, 10, 2);
            service.CreateNewStock(StockType.Equity, 10, 2);
            service.CreateNewStock(StockType.Equity, 10, 2);
            service.CreateNewStock(StockType.Bond, 10, 2);
            service.CreateNewStock(StockType.Bond, 10, 2);
        }
        
        [TestMethod]
        public void CanGetTotalMarketValue()
        {
            var service = new StockService();
            TestInputForTotalsTest(service);
            var total = service.GetTotalMarketTest();

            Assert.AreEqual(100, total);
        }

        [TestMethod]
        public void CanGetTotalMarketValueByEquity()
        {
            var service = new StockService();
            TestInputForTotalsTest(service);
            var total = service.GetTotalMarketTest(StockType.Equity);

            Assert.AreEqual(40, total);
        }

        [TestMethod]
        public void CanGetTotalMarketValueByBond()
        {
            var service = new StockService();
            TestInputForTotalsTest(service);
            var total = service.GetTotalMarketTest(StockType.Bond);

            Assert.AreEqual(60, total);
        }

        [TestMethod]
        public void CanGetTotalStockWeight()
        {
            var service = new StockService();
            TestInputForTotalsTest(service);
            var total = service.GetTotalStockWeight();

            Assert.AreEqual(100, total);
        }

        [TestMethod]
        public void CanGetTotalStockWeightByEquity()
        {
            var service = new StockService();
            TestInputForTotalsTest(service);
            var total = service.GetTotalStockWeight(StockType.Equity);

            Assert.AreEqual(40, total);
        }

        [TestMethod]
        public void CanGetTotalStockWeightByBond()
        {
            var service = new StockService();
            TestInputForTotalsTest(service);
            var total = service.GetTotalStockWeight(StockType.Bond);

            Assert.AreEqual(60, total);
        }

        [DataTestMethod]
        [DataRow(StockType.Equity, 1.00, 1, 0.005)]
        [DataRow(StockType.Equity, 12.00, 2, 0.12)]
        [DataRow(StockType.Equity, 10.00, 10, 0.50)]
        [DataRow(StockType.Bond, 1.00, 1, 0.02)]
        [DataRow(StockType.Bond, 12.00, 2, 0.48)]
        [DataRow(StockType.Bond, 10.00, 10, 2)]
        public void TestTransactionCost(StockType type, double price, int quantity, double expectedTransactionCost)
        {
            var service = new StockService();
            service.CreateNewStock(type, (decimal)price, quantity);

            var dto = service.GetDTO(1);

            Assert.AreEqual((decimal)expectedTransactionCost, dto.TransactionCost);
        }

        [TestMethod]
        public void TestStockWeight()
        {
            var service = new StockService();

            service.CreateNewStock(StockType.Bond, 2.0M, 10);
            service.CreateNewStock(StockType.Bond, 25.0M, 2);
            service.CreateNewStock(StockType.Bond, 6.0M, 5);

            var dtos = service.ListDTO();

            Assert.AreEqual(20, dtos[0].StockWeight);
            Assert.AreEqual(50, dtos[1].StockWeight);
            Assert.AreEqual(30, dtos[2].StockWeight);
        }

        [DataTestMethod]
        [DataRow(StockType.Equity, -2, 1, true)]
        [DataRow(StockType.Bond, -2, 1, true)]
        [DataRow(StockType.Equity, 20, 5, false)]
        [DataRow(StockType.Bond, 20, 5, false)]
        [DataRow(StockType.Equity, 22_000_000, 1, false)]
        [DataRow(StockType.Bond, 5_100_000, 5, true)]
        [DataRow(StockType.Equity, 42_000_000, 5, true)]
        public void TestIfShouldHighlight(StockType type, double price, int quantity, bool highlight)
        {
            var service = new StockService();
            service.CreateNewStock(type, (decimal)price, quantity);
            
            var dto = service.GetDTO(1);
            Assert.AreEqual(highlight, dto.Highlight);
        }

        [TestMethod]
        public void TestNameEquity()
        {
            var service = new StockService();

            var stock = service.CreateNewStock(StockType.Equity, 1, 1);

            Assert.AreEqual("Equity1", stock.Name);
        }

        [TestMethod]
        public void TestNameBond()
        {
            var service = new StockService();

            var stock = service.CreateNewStock(StockType.Bond, 1, 1);

            Assert.AreEqual("Bond1", stock.Name);
        }

        [TestMethod]
        public void TestNameBondEquity()
        {
            var service = new StockService();

            var stock1 = service.CreateNewStock(StockType.Bond, 1, 1);
            var stock2 = service.CreateNewStock(StockType.Equity, 1, 1);

            Assert.AreEqual("Bond1", stock1.Name);
            Assert.AreEqual("Equity1", stock2.Name);
        }

        [TestMethod]
        public void TestNameEquityBond()
        {
            var service = new StockService();

            var stock1 = service.CreateNewStock(StockType.Equity, 1, 1);
            var stock2 = service.CreateNewStock(StockType.Bond, 1, 1);

            Assert.AreEqual("Equity1", stock1.Name);
            Assert.AreEqual("Bond1", stock2.Name);
        }

        [TestMethod]
        public void TestNameBondEquityBondEquity()
        {
            var service = new StockService();

            var stock1 = service.CreateNewStock(StockType.Bond, 1, 1);
            var stock2 = service.CreateNewStock(StockType.Equity, 1, 1);
            var stock3 = service.CreateNewStock(StockType.Bond, 1, 1);
            var stock4 = service.CreateNewStock(StockType.Equity, 1, 1);
            
            Assert.AreEqual("Bond1", stock1.Name);
            Assert.AreEqual("Equity1", stock2.Name);
            Assert.AreEqual("Bond2", stock3.Name);
            Assert.AreEqual("Equity2", stock4.Name);
        }
    }
}
