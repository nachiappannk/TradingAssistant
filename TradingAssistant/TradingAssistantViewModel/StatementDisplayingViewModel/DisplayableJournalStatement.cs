﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Nachiappan.TradingAssistantViewModel.Model.Statements;

namespace Nachiappan.TradingAssistantViewModel.StatementDisplayingViewModel
{
    public class DisplayableJournalStatement
    {

        public DisplayableJournalStatement(JournalStatement x)
        {
            Account = x.Account;
            Date = x.Date;
            Description = x.Description;
            Tag = x.Tag;
            Credit = x.GetCreditValueOrNull();
            Debit = x.GetDebitValueOrNull();
        }

        public DisplayableJournalStatement()
        {
            Account = null;
            Date = null;
            Description =null;
            Tag = null;
            Credit = null;
            Debit = null;
        }


        [DisplayName("Date")]
        [DisplayFormat(DataFormatString = CommonDefinition.DateDisplayFormat)]
        public DateTime? Date { get; set; }

        [DisplayName("Account")]
        public string Account { get; set; }

        [DisplayName("Tag")]
        public string Tag { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayFormat(DataFormatString = CommonDefinition.ValueDisplayFormat)]
        [DisplayName("Credit")]
        public double? Credit { get; set; }


        [DisplayFormat(DataFormatString = CommonDefinition.ValueDisplayFormat)]
        [DisplayName("Debit")]
        public double? Debit { get; set; }
        
    }
}