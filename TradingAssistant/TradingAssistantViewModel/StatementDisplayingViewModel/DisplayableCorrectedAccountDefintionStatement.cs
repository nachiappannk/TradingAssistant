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

    public class DisplayableCorrectedTradeStatement
    {
        [DisplayFormat(DataFormatString = CommonDefinition.DateDisplayFormat)]
        public DateTime Date { get; set; }
        public string Name { get; set; }

        [DisplayName("Transaction Detail")]
        public string TransactionDetail { get; set; }

        [DisplayName("Transaction Tax")]
        public string TransactionTax { get; set; }

        [DisplayFormat(DataFormatString = CommonDefinition.QuantityDisplayFormat)]
        public double Quanity { get; set; }

        [DisplayFormat(DataFormatString = CommonDefinition.ValueDisplayFormat)]
        public double Cost { get; set; }

        [DisplayFormat(DataFormatString = CommonDefinition.ValueDisplayFormat)]
        public double Sale { get; set; }

        public string Reason { get; set; }

        public DisplayableCorrectedTradeStatement(AdjustedTradeStatement tradeStatement)
        {
            Date = tradeStatement.Date;
            TransactionDetail = tradeStatement.TransactionDetail;
            TransactionTax = tradeStatement.TransactionTax;
            Quanity = tradeStatement.Quanity;
            Cost = (tradeStatement.Value < 0) ? tradeStatement.Value * -1 : 0;
            Sale = (tradeStatement.Value >= 0) ? tradeStatement.Value : 0;
            Name = tradeStatement.Name;
            Reason = tradeStatement.Reason;
           
        }
    }
}