using System;
using System.Collections.Generic;
using System.Linq;
using FundManager.Domain.Entities;
using FundManager.Repository;
using FundManager.Service.DTO;

namespace FundManager.Service
{
    public class StockService
    {
        #region Static Methods

        /// <summary>
        /// Tests if, given the values, the entry should be highlighted
        /// </summary>
        private static bool ShouldHighlight(decimal marketValue, decimal tolerance, decimal transactionCost)
        {
            return marketValue < 0
                   || transactionCost > tolerance;
        }

        /// <summary>
        /// Calculates the entry's Stock Weight
        /// </summary>
        private static decimal CalculateStockWeight(decimal marketValue, decimal totalMarketValue)
        {
            if (totalMarketValue == 0) { return 0; }

            return marketValue / totalMarketValue * 100;
        }

        /// <summary>
        /// Calculates the entry's transaction cost
        /// </summary>
        private static decimal CalculateTransactionCost(decimal marketValue, decimal transactionCostFactor)
        {
            return marketValue * transactionCostFactor;
        }

        #endregion

        private readonly StockRepository repository;
        
        public StockService()
        {
            repository = new StockRepository();
        }

        private List<StockDTO> ConvertToDTOList(List<Stock> stockList)
        {
            decimal equityTransactionCostFactor = 0.005M; // 0.5%
            decimal equityTolerance = 200_000;

            decimal bondTransactionCostFactor = 0.02M; // 2%;
            decimal bondTolerance = 100_000;

            List<StockDTO> resultList = new List<StockDTO>();

            decimal totalMarketValue = stockList.Sum(x => x.MarketValue);

            foreach (var stock in stockList)
            {
                decimal transactionCostFactor;
                decimal tolerance;

                if (stock.Type == StockType.Equity)
                {
                    transactionCostFactor = equityTransactionCostFactor;
                    tolerance = equityTolerance;
                }
                else
                {
                    transactionCostFactor = bondTransactionCostFactor;
                    tolerance = bondTolerance;
                }

                decimal transactionCost = CalculateTransactionCost(stock.MarketValue, transactionCostFactor);
                decimal stockWeight = CalculateStockWeight(stock.MarketValue, totalMarketValue);
                bool highlight = ShouldHighlight(stock.MarketValue, tolerance, transactionCost);

                var stockDTO = new StockDTO(stock, transactionCost, stockWeight, highlight);
                resultList.Add(stockDTO);
            }

            return resultList;
        }

        public int Count(Func<Stock, bool> predicate = null)
        {
            return repository.Count(predicate);
        }

        public decimal GetTotalMarketTest(StockType? type = null)
        {
            var data = repository.AsQueryable();

            if (type != null)
            {
                data = data.Where(x => x.Type == type.Value);
            }

            return data.Sum(x => x.MarketValue);
        }

        public decimal GetTotalStockWeight(StockType? type = null)
        {
            IEnumerable<StockDTO> data = ListDTO();

            if (type != null)
            {
                data = data.Where(x => x.Type == type.Value);
            }

            return data.Sum(x => x.StockWeight);
        }

        public StockDTO GetDTO(int id)
        {
            var stock = repository.Get(id);
            if (stock == null) { return null; }

            var dto = ConvertToDTOList(new List<Stock> { stock })[0];

            return dto;
        }

        public List<StockDTO> ListDTO()
        {
            var list = repository.List();
            var dtoList = ConvertToDTOList(list);
            return dtoList;
        }

        public StockDTO CreateNewStock(StockType type, decimal price, int quantity)
        {
            string namePrefix;

            if (type == StockType.Equity)
            {
                namePrefix = "Equity";
            }
            else
            {
                namePrefix = "Bond";
            }

            Stock stock = new Stock
            {
                Name = namePrefix + (repository.Count(x => x.Type == type) + 1),
                Type = type,
                Price = price,
                Quantity = quantity
            };

            repository.Insert(stock);

            return ConvertToDTOList(new List<Stock> { stock })[0];
        }

    }
}
