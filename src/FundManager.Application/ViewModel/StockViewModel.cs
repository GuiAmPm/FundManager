using FundManager.Application.Application.Helpers;
using FundManager.Domain.Entities;
using FundManager.Service;
using FundManager.Service.DTO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace FundManager.Application.ViewModel
{
    public class StockViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private StockService _stockService;
        private List<StockDTO> _stockList;

        private int _totalCount;
        private decimal _totalMarketValue, _totalStockWeight;

        private int _totalEquityCount;
        private decimal _totalEquityMarketValue, _totalEquityStockWeight;

        private int _totalBondCount;
        private decimal _totalBondMarketValue, _totalBondStockWeight;

        private double _inputPrice = 10;
        private int _inputQuantity = 1;
        private int _inputType = 0;

        public List<StockDTO> StockList
        {
            get => _stockList;
            set
            {
                if (_stockList != value)
                {
                    _stockList = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StockList)));
                }
            }
        }

        public ICommand InsertStockToListCommand { get; private set; }

        public double InputPrice
        {
            get => _inputPrice;
            set
            {
                _inputPrice = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(InputPrice)));
            }
        }

        public int InputQuantity
        {
            get => _inputQuantity;
            set
            {
                _inputQuantity = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(InputQuantity)));
            }
        }

        public int InputType
        {
            get => _inputType;
            set
            {
                _inputType = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(InputType)));
            }
        }

        public int TotalCount
        {
            get => _totalCount;
            set
            {
                _totalCount = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TotalCount)));
            }
        }

        public int TotalEquityCount
        {
            get => _totalEquityCount;
            set
            {
                _totalEquityCount = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TotalEquityCount)));
            }
        }

        public int TotalBondCount
        {
            get => _totalBondCount;
            set
            {
                _totalBondCount = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TotalBondCount)));
            }
        }

        public decimal TotalMarketValue
        {
            get => _totalMarketValue;
            set
            {
                _totalMarketValue = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TotalMarketValue)));
            }
        }

        public decimal TotalEquityMarketValue
        {
            get => _totalEquityMarketValue;
            set
            {
                _totalEquityMarketValue = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TotalEquityMarketValue)));
            }
        }

        public decimal TotalBondMarketValue
        {
            get => _totalBondMarketValue;
            set
            {
                _totalBondMarketValue = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TotalBondMarketValue)));
            }
        }

        public decimal TotalStockWeight
        {
            get => _totalStockWeight;
            set
            {
                _totalStockWeight = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TotalStockWeight)));
            }
        }

        public decimal TotalEquityStockWeight
        {
            get => _totalEquityStockWeight;
            set
            {
                _totalEquityStockWeight = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TotalEquityStockWeight)));
            }
        }

        public decimal TotalBondStockWeight
        {
            get => _totalBondStockWeight;
            set
            {
                _totalBondStockWeight = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TotalBondStockWeight)));
            }
        }
        

        public StockViewModel()
        {
            _stockService = new StockService();
            _stockList = _stockService.ListDTO();

            InsertStockToListCommand = new ActionCommand(OnStockInsertHandler);
        }
        
        private void OnStockInsertHandler()
        {
            StockType type;
            if (_inputType == 0) { type = StockType.Equity; }
            else { type = StockType.Bond; }

            _stockService.CreateNewStock(type, (decimal)InputPrice, InputQuantity);

            StockList = _stockService.ListDTO();

            TotalCount = _stockService.Count();
            TotalEquityCount = _stockService.Count(x => x.Type == StockType.Equity);
            TotalBondCount = _stockService.Count(x => x.Type == StockType.Bond);

            TotalMarketValue = _stockService.GetTotalMarketTest();
            TotalEquityMarketValue = _stockService.GetTotalMarketTest(StockType.Equity);
            TotalBondMarketValue = _stockService.GetTotalMarketTest(StockType.Bond);

            TotalStockWeight = _stockService.GetTotalStockWeight();
            TotalEquityStockWeight = _stockService.GetTotalStockWeight(StockType.Equity);
            TotalBondStockWeight = _stockService.GetTotalStockWeight(StockType.Bond);
        }
        
        public string Error => null;
        
        public string this[string columnName]
        {
            get
            {
                if (columnName == nameof(InputPrice))
                {
                    if (InputPrice == 0)
                    {
                        return "The stock price is required.";
                    }
                }
                if (columnName == nameof(InputQuantity))
                {
                    if (InputQuantity == 0)
                    {
                        return "The stock quantity is required.";
                    }
                }

                return null;
            }
        }
    }
}