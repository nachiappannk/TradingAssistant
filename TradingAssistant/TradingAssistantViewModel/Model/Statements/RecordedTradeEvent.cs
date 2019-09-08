using System;

namespace Nachiappan.TradingAssistantViewModel.Model.Statements
{
    public class RecordedTradeEvent : IHasValue
    {
        public int SerialNumber { get; set; }
        public string SerialNumberString { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string TransactionDetail { get; set; }
        public string TransactionTax { get; set; }
        public double? Quanity { get; set; }
        public double Value { get; set; }
        public double? CostValue { get; set; }
        public double? SaleValue { get; set; }
    }

    public class CleanedTradeEvent : RecordedTradeEvent
    {
        public string Reason { get; set; }
        public bool IsValid { get; set; }
    }




    public class AdjustedTradeStatement : RecordedTradeEvent, IHasReason
    {
        public string Reason { get; set; }
    }

    public interface IHasReason
    {
        string Reason { get; set; }
    }

    public static class HasReasonExtentions
    {
        public static void AddReason(this IHasReason hasReason, string reason)
        {
            if (!string.IsNullOrEmpty(hasReason.Reason)) hasReason.Reason = hasReason.Reason + " ";
            hasReason.Reason = hasReason.Reason + reason;
        }
    }

}