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

    public class CleanedTradeEvent : RecordedTradeEvent
    {
        public CleanedTradeEvent(RecordedTradeEvent tradeEvent)
        {
            Reason = string.Empty;
            IsValid = true;
            IsAdjusted = false;

            SerialNumber = tradeEvent.SerialNumber;
            SerialNumberString = tradeEvent.SerialNumberString;
            Date = tradeEvent.Date;
            Name = tradeEvent.Name;
            TransactionDetail = tradeEvent.TransactionDetail;
            TransactionTax = tradeEvent.TransactionTax;
            Quanity = tradeEvent.Quanity;
            CostValue = tradeEvent.CostValue;
            SaleValue = tradeEvent.SaleValue;

        }

        public string Reason { get; set; }
        public bool IsValid { get; set; }
        public bool IsAdjusted { get; set; }

        public void AddReason(string reason)
        {
            if (!string.IsNullOrEmpty(Reason)) Reason = Reason + " ";
            Reason = Reason + reason;
        }
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