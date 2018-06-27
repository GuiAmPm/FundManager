using FundManager.Domain.Entities;
using FundManager.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace FundManager.Tests.Repository
{
    [TestClass]
    public class StockRepositoryTest
    {
        [TestMethod]
        public void CanCreateRepository()
        {
            var repository = new StockRepository();
        }
        

        [TestMethod]
        public void CanList()
        {
            var repository = new StockRepository();
            var list = repository.List();

            Assert.IsNotNull(list);
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void ListAfterOneInsertReturnsOne()
        {
            var repository = new StockRepository();

            repository.Insert(new Stock());

            var list = repository.List();

            Assert.AreEqual(1, list.Count);
        }

        [TestMethod]
        public void ListsAreNotTheSame()
        {
            var repository = new StockRepository();

            var listA = repository.List();
            var listB = repository.List();

            Assert.AreNotSame(listA, listB);
        }

        [TestMethod]
        public void CanCount()
        {
            var repository = new StockRepository();
            var count = repository.Count();

            Assert.AreEqual(0, count);
        }

        [TestMethod]
        public void CanCountWithPredicate()
        {
            var repository = new StockRepository();
            var count = repository.Count(x => 1 == 0);

            Assert.AreEqual(0, count);
        }
        
        [TestMethod]
        public void CanCountWithPredicateAfterInsert()
        {
            var repository = new StockRepository();
            repository.Insert(new Stock());

            var count = repository.Count(x => 1 == 0);
            
            Assert.AreEqual(0, count);
        }

        [TestMethod]
        public void CanGet()
        {
            var repository = new StockRepository();
            var stock = repository.Get(1);

            Assert.IsNull(stock);
        }

        [TestMethod]
        public void CanInsert()
        {
            var repository = new StockRepository();
            var stock = new Stock();
            repository.Insert(stock);

            Assert.AreEqual(1, repository.Count());
        }
        
        [TestMethod]
        public void CanNotInsertTheSameItemTwice()
        {
            var repository = new StockRepository();
            var stock = new Stock();

            repository.Insert(stock);
            repository.Insert(stock);

            Assert.AreEqual(1, repository.Count());
        }

        [TestMethod]
        public void InsertPersists()
        {
            var repository = new StockRepository();
            var stock = new Stock();
            repository.Insert(stock);

            Assert.AreEqual(1, repository.Count());
        }

        [TestMethod]
        public void CanInsertList()
        {
            var repository = new StockRepository();
            var stockList = new List<Stock> { new Stock(), new Stock() };
            repository.InsertList(stockList);

            Assert.AreEqual(2, repository.Count());
        }

        [TestMethod]
        public void IdIsAssignedOnInsert()
        {
            var repository = new StockRepository();
            repository.Insert(new Stock());
            
            Assert.IsNotNull(repository.Get(1));
        }

        [TestMethod]
        public void IdZeroReturnsNullAfterOneInsert()
        {
            var repository = new StockRepository();
            repository.Insert(new Stock());

            Assert.IsNull(repository.Get(0));
        }


        [TestMethod]
        public void IdTwoReturnsNullAfterOneInsert()
        {
            var repository = new StockRepository();
            repository.Insert(new Stock());

            Assert.IsNull(repository.Get(2));
        }

        [TestMethod]
        public void IdIsAssignedOnInsertList()
        {
            var repository = new StockRepository();
            repository.InsertList(new List<Stock> { new Stock(), new Stock() });
            
            Assert.IsNotNull(repository.Get(1));
            Assert.IsNotNull(repository.Get(2));
        }

        [TestMethod]
        public void CanGetAsQueryable()
        {
            var repository = new StockRepository();
            var queryable = repository.AsQueryable();

            Assert.IsNotNull(queryable);
            Assert.IsNull(queryable as List<Stock>);
        }
    }
}
