using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nachiappan.TradingAssistantViewModel.Model.Statements
{
    public abstract class PortfolioEvent
    {
    }

    public class PurchaseEvent : PortfolioEvent
    {
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string TransactionDetail { get; set; }
        public string TransactionTax { get; set; }
        public double Quanity { get; set; }
        public double CostValue { get; set; }
    }

    public class SaleEvent : PortfolioEvent
    {
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string TransactionDetail { get; set; }
        public string TransactionTax { get; set; }
        public double Quanity { get; set; }
        public double SaleValue { get; set; }
    }
}
