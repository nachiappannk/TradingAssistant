using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nachiappan.TradingAssistantViewModel.Model;
using Nachiappan.TradingAssistantViewModel.Model.Statements;
using NUnit.Framework;

namespace Nachiappan.TradingAssistantViewModel.Tests
{
    [TestFixture]
    public class LedgerClassiferTests
    {
        public void Test()
        {

        }

    }

    [TestFixture]
    public class PortfolioTests
    {
        [Test]
        public void BasicPurchaseTest()
        {
            var portfolio = new Portfolio();
            portfolio.HandleEvent(new PurchaseEvent()
            {
                Date = new DateTime(2019,1,1),
                Name =  "a",
                TransactionDetail = "t1",
                TransactionTax = "t2",
                Quanity = 4,
                CostValue = 455,
            });

            var holdingStatements = portfolio.GetHoldingStatements();
            Assert.AreEqual(1, holdingStatements.Count);
            var record = holdingStatements.ElementAt(0);
            Assert.AreEqual("a", record.Name);
            Assert.AreEqual("t1", record.TransactionDetail);
            Assert.AreEqual("t2", record.TransactionTax);
            Assert.AreEqual(4, record.Quanity, 0.001);
            Assert.AreEqual(455, record.CostValue, 0.001);
        }
    }
}