using System;
using System.ComponentModel;
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

        public DateTime Date { get; set; }
        public string Name { get; set; }

        [DisplayName("Transaction Detail")]
        public string TransactionDetail { get; set; }

        [DisplayName("Transaction Tax")]
        public string TransactionTax { get; set; }
        public double Quanity { get; set; }
        public double Credit { get; set; }
        public double Debit { get; set; }
        public string Reason { get; set; }

        public DisplayableCorrectedTradeStatement(TradeStatement tradeStatement)
        {
            Date = tradeStatement.Date;
            Name = tradeStatement.Name;
            TransactionDetail = tradeStatement.TransactionDetail;
            TransactionTax = tradeStatement.TransactionTax;
            Quanity = tradeStatement.Quanity;
            Debit = (tradeStatement.Value < 0) ? tradeStatement.Value * -1 : 0;
            Credit = (tradeStatement.Value >= 0) ? tradeStatement.Value : 0;
            if (Name.Contains("##")){

            }
            else {

            }
        }
    }
}