namespace Nachiappan.TradingAssistantViewModel.Model.Statements
{
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
}