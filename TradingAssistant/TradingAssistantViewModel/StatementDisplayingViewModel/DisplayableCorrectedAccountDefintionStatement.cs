﻿using System;
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

        public DisplayableCorrectedTradeStatement(AdjustedTradeStatement recordedTradeEvent)
        {
            Date = recordedTradeEvent.Date;
            TransactionDetail = recordedTradeEvent.TransactionDetail;
            TransactionTax = recordedTradeEvent.TransactionTax;
            Quanity = recordedTradeEvent.Quanity.HasValue ? recordedTradeEvent.Value:0;
            Cost = (recordedTradeEvent.Value < 0) ? recordedTradeEvent.Value * -1 : 0;
            Sale = (recordedTradeEvent.Value >= 0) ? recordedTradeEvent.Value : 0;
            Name = recordedTradeEvent.Name;
            Reason = recordedTradeEvent.Reason;
           
        }
    }
}