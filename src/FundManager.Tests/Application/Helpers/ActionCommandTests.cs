using System;
using FundManager.Application.Application.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FundManager.Tests.Application.Helpers
{
    [TestClass]
    public class ActionCommandTests
    {
        [TestMethod]
        public void CanCreateCommand()
        {
            var command = new ActionCommand(()=> { });
        }

        [TestMethod]
        public void NullActionCommandTest()
        {
            try
            {
                var command = new ActionCommand(null);
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(ArgumentNullException));
                return;
            }

            Assert.Fail();
        }

        [TestMethod]
        public void CanExecuteTest()
        {
            var command = new ActionCommand(() => { });
            Assert.IsTrue(command.CanExecute(null));
        }

        [TestMethod]
        public void ExecuteTest()
        {
            bool executed = false;

            var command = new ActionCommand(()=>
            {
                executed = true;
            });

            command.Execute(null);

            Assert.IsTrue(executed);
        }
    }
}
