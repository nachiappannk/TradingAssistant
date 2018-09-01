﻿using System;

namespace Nachiappan.TradingAssistantViewModel.Model.Statements
{
    public class TradeStatement : IHasValue, ICanClone<TradeStatement>
    {
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string TransactionDetail { get; set; }
        public string TransactionTax { get; set; }
        public double Quanity { get; set; }
        public double Value { get; set; }
        public TradeStatement Clone()
        {
            return new TradeStatement()
            {
                Date= Date,
                Name = Name,
                TransactionDetail = TransactionDetail,
                TransactionTax = TransactionTax,
                Quanity = Quanity,
                Value =Value,
            };
        }
    }
}