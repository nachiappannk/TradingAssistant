using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;

namespace Nachiappan.TradingAssistantViewModel.Model.Statements
{
    public class Asset
    {
        public string Name { get; set; }

        private List<AssetHoldingStatement> _holdingStatements = new List<AssetHoldingStatement>();

        public List<AssetHoldingStatement> GetAssetHoldingStatements()
        {
            return _holdingStatements;
        }

        public Asset(string name)
        {
            Name = name;
        }

        public void Purchase(DateTime date, string transactionDetails, string transactionTax, double quantity, double cost)
        {
            _holdingStatements.Add(new AssetHoldingStatement()
            {
                Date = date,
                TransactionDetail = transactionDetails,
                TransactionTax = transactionTax,
                Quanity = quantity,
                CostValue = cost,
            });
        }

        public void Sell(DateTime date, string transactionDetail, string transactionTax, double quanity, double value)
        {
            var saleQuantity = quanity;
            var saleValue = value;
            while (saleQuantity > 0.001)//TODO 0.001 defined in multiple places
            {
                var holding = _holdingStatements.ElementAt(0);
                _holdingStatements.RemoveAt(0);
                if (holding.Quanity > saleQuantity)
                {
                    var gainStatement = new GainStatement()
                    {
                        Quanity = saleQuantity,
                        PurchaseDate = holding.Date,
                        PurchaseTransactionDetail = holding.TransactionDetail,
                        PurchaseTransactionTax = holding.TransactionTax,
                        PurchaseValue = holding.CostValue * saleQuantity / holding.Quanity,
                        SaleDate = date,
                        SaleValue = saleValue,
                        SaleTransactionDetail = transactionDetail,
                        SaleTransactionTax = transactionTax,
                    };
                    saleQuantity = 0;
                    holding.Quanity = holding.Quanity - gainStatement.Quanity;
                    holding.CostValue = holding.CostValue - gainStatement.PurchaseValue;
                    _holdingStatements.Insert(0, holding);
                    //TODO insert Gain Staement
                }
                else
                {
                    
                    var gainStatement = new GainStatement()
                    {
                        Quanity = holding.Quanity,
                        PurchaseDate = holding.Date,
                        PurchaseTransactionDetail = holding.TransactionDetail,
                        PurchaseTransactionTax = holding.TransactionTax,
                        PurchaseValue = holding.CostValue,
                        SaleDate = date,
                        SaleValue = saleValue * holding.Quanity / saleQuantity,
                        SaleTransactionDetail = transactionDetail,
                        SaleTransactionTax = transactionTax,
                    };
                    saleQuantity = saleQuantity - gainStatement.Quanity;
                    saleValue = saleValue - gainStatement.SaleValue;
                    //TODO insert Gain Staement
                }
            }
        }
    }
}