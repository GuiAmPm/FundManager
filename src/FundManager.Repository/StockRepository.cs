using FundManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FundManager.Repository
{
    /// <summary>
    /// A simple, volatile repository, it assigns an ID to each added entry and ignores repeated entries 
    /// </summary>
    public class StockRepository
    {
        private int idTracker = 0;
        private readonly List<Stock> _internalStore;

        public StockRepository()
        {
            _internalStore = new List<Stock>();
        }

        public List<Stock> List()
        {
            return new List<Stock>(_internalStore);
        }

        public int Count(Func<Stock, bool> predicate = null)
        {
            if (predicate != null)
            {
                return _internalStore.Count(predicate);
            }

            return _internalStore.Count;
        }

        public Stock Get(int id)
        {
            return _internalStore.FirstOrDefault(x=>x.Id == id);
        }

        public void Insert(Stock stock)
        {
            if(_internalStore.Contains(stock))
            {
                return;
            }

            stock.Id = ++idTracker;
            _internalStore.Add(stock);
        }

        public void InsertList(List<Stock> stockList)
        {
            foreach(var stock in stockList)
            {
                Insert(stock);
            }
        }

        public IQueryable<Stock> AsQueryable()
        {
            return _internalStore.AsQueryable();
        }
    }
}