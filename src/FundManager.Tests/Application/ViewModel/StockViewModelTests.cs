using FundManager.Application.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Input;

namespace FundManager.Tests.Application.ViewModel
{
    [TestClass]
    public class StockViewModelTests
    {
        [TestMethod]
        public void CanCreateViewModel()
        {
            var viewModel = new StockViewModel();
        }

        [TestMethod]
        public void HasStockList()
        {
            var viewModel = new StockViewModel();
            var stockList = viewModel.StockList;

            Assert.IsNotNull(stockList);
        }

        [TestMethod]
        public void HasInputPrice()
        {
            var viewModel = new StockViewModel();
            var price = viewModel.InputPrice;
        }

        [TestMethod]
        public void HasValidInputPrice()
        {
            var viewModel = new StockViewModel
            {
                InputPrice = 10
            };

            Assert.IsNull(viewModel[nameof(StockViewModel.InputPrice)]);
        }

        [TestMethod]
        public void HasZeroInputPrice()
        {
            var viewModel = new StockViewModel
            {
                InputPrice = 0
            };

            Assert.IsNotNull(viewModel[nameof(StockViewModel.InputPrice)]);
        }

        [TestMethod]
        public void HasInputQuantity()
        {
            var viewModel = new StockViewModel();
            var quantity = viewModel.InputQuantity;
        }

        [TestMethod]
        public void HasValidInputQuantity()
        {
            var viewModel = new StockViewModel
            {
                InputQuantity = 10
            };

            Assert.IsNull(viewModel[nameof(StockViewModel.InputQuantity)]);
        }

        [TestMethod]
        public void HasZeroInputQuantity()
        {
            var viewModel = new StockViewModel
            {
                InputQuantity = 0
            };

            Assert.IsNotNull(viewModel[nameof(StockViewModel.InputQuantity)]);
        }

        [TestMethod]
        public void HasInputType()
        {
            var viewModel = new StockViewModel();
            var typeIndex = viewModel.InputType;

            Assert.AreEqual(0, typeIndex);
        }
        [TestMethod]
        public void CanExecuteInsertStockToListCommand()
        {
            var viewModel = new StockViewModel();
            ICommand command = viewModel.InsertStockToListCommand;
            command.Execute(null);
        }
        
        [TestMethod]
        public void NotifyListUpdatedFiredTest()
        {
            var viewModel = new StockViewModel();

            bool listUpdated = false;

            viewModel.PropertyChanged += (sender, arguments) =>
            {
                if (arguments.PropertyName == nameof(StockViewModel.StockList))
                {
                    listUpdated = true;
                }
            };

            ICommand command = viewModel.InsertStockToListCommand;
            command.Execute(null);

            Assert.IsTrue(listUpdated);
        }

        [TestMethod]
        public void InputPriceFireChanged()
        {
            var viewModel = new StockViewModel();
            var hasChanged = false;

            viewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(StockViewModel.InputPrice))
                {
                    hasChanged = true;
                }
            };

            viewModel.InputPrice = 10;

            Assert.IsTrue(hasChanged);
        }

        [TestMethod]
        public void InputQuantityFireChanged()
        {
            var viewModel = new StockViewModel();
            var hasChanged = false;

            viewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(StockViewModel.InputQuantity))
                {
                    hasChanged = true;
                }
            };

            viewModel.InputQuantity = 10;

            Assert.IsTrue(hasChanged);
        }

        [TestMethod]
        public void InputTypeFireChanged()
        {
            var viewModel = new StockViewModel();
            var hasChanged = false;

            viewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(StockViewModel.InputType))
                {
                    hasChanged = true;
                }
            };

            viewModel.InputType = 1;

            Assert.IsTrue(hasChanged);
        }

        [DataTestMethod]
        [DataRow(nameof(StockViewModel.TotalCount))]
        [DataRow(nameof(StockViewModel.TotalEquityCount))]
        [DataRow(nameof(StockViewModel.TotalBondCount))]

        [DataRow(nameof(StockViewModel.TotalMarketValue))]
        [DataRow(nameof(StockViewModel.TotalEquityMarketValue))]
        [DataRow(nameof(StockViewModel.TotalBondMarketValue))]

        [DataRow(nameof(StockViewModel.TotalStockWeight))]
        [DataRow(nameof(StockViewModel.TotalEquityStockWeight))]
        [DataRow(nameof(StockViewModel.TotalBondStockWeight))]
        public void NotifyWhenPropertyChanges(string propName)
        {
            var viewModel = new StockViewModel();

            bool hasChanged = false;

            viewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == propName)
                {
                    hasChanged = true;
                }
            };

            var command = viewModel.InsertStockToListCommand;
            command.Execute(null);

            Assert.IsTrue(hasChanged);
        }
    }
}
