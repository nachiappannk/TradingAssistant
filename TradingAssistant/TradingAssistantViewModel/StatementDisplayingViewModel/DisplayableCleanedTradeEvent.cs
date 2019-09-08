using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Nachiappan.TradingAssistantViewModel.Model.Statements;

namespace Nachiappan.TradingAssistantViewModel.StatementDisplayingViewModel
{
    public class DisplayableCleanedTradeEvent
    {
        public int SerialNumber { get; set; }

        [DisplayName("Serial Number (Actual)")]
        public string SerialNumberString { get; set; }

        [DisplayFormat(DataFormatString = CommonDefinition.DateDisplayFormat)]
        public DateTime Date { get; set; }
        public string Name { get; set; }

        [DisplayName("Transaction Detail")]
        public string TransactionDetail { get; set; }

        [DisplayName("Transaction Tax")]
        public string TransactionTax { get; set; }

        [DisplayFormat(DataFormatString = CommonDefinition.QuantityDisplayFormat)]
        public double? Quanity { get; set; }

        [DisplayFormat(DataFormatString = CommonDefinition.ValueDisplayFormat)]
        public double? Cost { get; set; }

        [DisplayFormat(DataFormatString = CommonDefinition.ValueDisplayFormat)]
        public double? Sale { get; set; }

        public bool IsAdjusted { get; set; }
        public bool IsValid { get; set; }

        public string Reason { get; set; }

        public DisplayableCleanedTradeEvent(CleanedTradeEvent cleanedTradeEvent, int serialNumber)
        {
            SerialNumber = serialNumber;
            SerialNumberString = cleanedTradeEvent.SerialNumberString;
            Date = cleanedTradeEvent.Date;
            TransactionDetail = cleanedTradeEvent.TransactionDetail;
            TransactionTax = cleanedTradeEvent.TransactionTax;
            Quanity = cleanedTradeEvent.Quanity;
            Cost = cleanedTradeEvent.CostValue;
            Sale = cleanedTradeEvent.SaleValue;
            Name = cleanedTradeEvent.Name;
            IsValid = cleanedTradeEvent.IsValid;
            IsAdjusted = cleanedTradeEvent.IsAdjusted;
            Reason = cleanedTradeEvent.Reason;
           
        }
    }
}