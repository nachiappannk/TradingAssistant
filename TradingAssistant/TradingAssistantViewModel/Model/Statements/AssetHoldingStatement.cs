using System;

namespace Nachiappan.TradingAssistantViewModel.Model.Statements
{
    public class AssetHoldingStatement
    {
        public DateTime Date { get; set; }
        public string TransactionDetail { get; set; }
        public string TransactionTax { get; set; }
        public double Quanity { get; set; }
        public double CostValue { get; set; }
    }

    public class HoldingStatement
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string TransactionDetail { get; set; }
        public string TransactionTax { get; set; }
        public double Quanity { get; set; }
        public double CostValue { get; set; }
    }

    public class AssetGainStatement
    {
        public DateTime PurchaseDate { get; set; }
        public string PurchaseTransactionDetail { get; set; }
        public string PurchaseTransactionTax { get; set; }
        public double Quanity { get; set; }
        public double CostValue { get; set; }
        public DateTime SaleDate { get; set; }
        public string SaleTransactionDetail { get; set; }
        public string SaleTransactionTax { get; set; }
        public double SaleValue { get; set; }
    }
}