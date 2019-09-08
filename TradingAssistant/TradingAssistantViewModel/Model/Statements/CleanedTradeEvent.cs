using System;
using OfficeOpenXml.FormulaParsing.Utilities;

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

        public PortfolioEvent GetPortfolioEvent()
        {
            if(!IsValid) throw new Exception("Something wend wrong");
            //TODO multiple places where the threshold is defined (0.001)
            if (!SaleValue.HasValue || SaleValue.Value < 0.001)
            {
                if (Quanity == null) throw new Exception("Something went wrong");
                if (!CostValue.HasValue || CostValue.Value < 0.001) throw new Exception("Something went wrong");
                return new PurchaseEvent()
                {
                    Date = this.Date,
                    Name = this.Name,
                    TransactionDetail = this.TransactionDetail,
                    TransactionTax = this.TransactionTax,
                    Quanity = Quanity.Value,
                    CostValue = CostValue.Value,
                };
            }
            if (!CostValue.HasValue || CostValue.Value < 0.001)
            {
                if (Quanity == null) throw new Exception("Something went wrong");
                if (!SaleValue.HasValue || SaleValue.Value < 0.001) throw new Exception("Something went wrong");
                return new SaleEvent()
                {
                    Date = this.Date,
                    Name = this.Name,
                    TransactionDetail = this.TransactionDetail,
                    TransactionTax = this.TransactionTax,
                    Quanity = Quanity.Value,
                    SaleValue = SaleValue.Value,
                };
            }
            throw new Exception("Something went wrong");
        }
    }
}