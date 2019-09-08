using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using Nachiappan.TradingAssistantViewModel.Model.Statements;

namespace Nachiappan.TradingAssistantViewModel.StatementDisplayingViewModel
{
    public class DisplayableCorrectedAccountDefintionStatement
    {
        public DisplayableCorrectedAccountDefintionStatement(CorrectedAccountDefintionStatement x)
        {
            AccountType = x.AccountType;
            Account = x.Account;
            RecipientAccount = x.RecipientAccount;
            Reason = x.Reason;
        }

        [DisplayName("Account Type")]
        public AccountType AccountType { get; set; }

        public string Account { get; set; }

        [DisplayName("Recipient Account")]
        public string RecipientAccount { get; set; }
        public string Reason { get; set; }
    }

    public class DisplayableCleanedTradeEvent
    {
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

        public string Reason { get; set; }

        public DisplayableCleanedTradeEvent(CleanedTradeEvent cleanedTradeEvent)
        {
            SerialNumberString = cleanedTradeEvent.SerialNumberString;
            Date = cleanedTradeEvent.Date;
            TransactionDetail = cleanedTradeEvent.TransactionDetail;
            TransactionTax = cleanedTradeEvent.TransactionTax;
            Quanity = cleanedTradeEvent.Quanity;
            Cost = cleanedTradeEvent.CostValue;
            Sale = cleanedTradeEvent.SaleValue;
            Name = cleanedTradeEvent.Name;
            Reason = cleanedTradeEvent.Reason;
           
        }
    }
}