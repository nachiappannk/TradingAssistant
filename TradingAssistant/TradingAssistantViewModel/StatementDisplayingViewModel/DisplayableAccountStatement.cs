﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Nachiappan.TradingAssistantViewModel.Model.Statements;

namespace Nachiappan.TradingAssistantViewModel.StatementDisplayingViewModel
{
    public class DisplayableAccountStatement
    {
        public DisplayableAccountStatement(AccountStatement x)
        {
            SerialNumber = x.SerialNumber;
            Description = x.Description;
            Date = x.Date;
            Credit = x.GetCreditValueOrNull();
            Debit = x.GetDebitValueOrNull();
            Balance = x.RunningTotaledValue;
        }

        [DisplayName("S.No.")]
        public int SerialNumber { get; set; }

        [DisplayName("Date")]
        [DisplayFormat(DataFormatString = CommonDefinition.DateDisplayFormat)]
        public DateTime Date { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayName("Credit")]
        [DisplayFormat(DataFormatString = CommonDefinition.ValueDisplayFormat)]
        public double? Credit { get; set; }

        [DisplayName("Debit")]
        [DisplayFormat(DataFormatString = CommonDefinition.ValueDisplayFormat)]
        public double? Debit { get; set; }

        [DisplayName("Balance")]
        [DisplayFormat(DataFormatString = CommonDefinition.ValueDisplayFormat)]
        public double Balance { get; set; }
    }
}