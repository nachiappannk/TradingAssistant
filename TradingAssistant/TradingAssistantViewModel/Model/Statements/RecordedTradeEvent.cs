using System;

namespace Nachiappan.TradingAssistantViewModel.Model.Statements
{
    public class RecordedTradeEvent
    {
        public int SerialNumber { get; set; }
        public string SerialNumberString { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string TransactionDetail { get; set; }
        public string TransactionTax { get; set; }
        public double? Quanity { get; set; }
        public double? CostValue { get; set; }
        public double? SaleValue { get; set; }
    }
}