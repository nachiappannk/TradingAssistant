using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nachiappan.TradingAssistantViewModel.Model.Statements
{
    public abstract class PortfolioEvent
    {
    }

    public class PurchaseEvent : PortfolioEvent
    {
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string TransactionDetail { get; set; }
        public string TransactionTax { get; set; }
        public double Quanity { get; set; }
        public double CostValue { get; set; }
    }

    public class SaleEvent : PortfolioEvent
    {
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string TransactionDetail { get; set; }
        public string TransactionTax { get; set; }
        public double Quanity { get; set; }
        public double SaleValue { get; set; }
    }

    public class Portfolio
    {
        Dictionary<string, Asset> _assets = new Dictionary<string, Asset>();

        public List<HoldingStatement> GetHoldingStatements()
        {
            return _assets.SelectMany(x => x.Value.GetAssetHoldingStatements(),
                (a, b) => new HoldingStatement()
                {
                    Name = a.Key,
                    Date = b.Date,
                    TransactionDetail = b.TransactionDetail,
                    TransactionTax = b.TransactionTax,
                    CostValue = b.CostValue,
                    Quanity = b.Quanity,
                }).ToList();
        }

        public void HandleEvent(PortfolioEvent portfolioEvent)
        {
            if (portfolioEvent is PurchaseEvent) HandlePurchaseEvent(portfolioEvent as PurchaseEvent);
            else if (portfolioEvent is SaleEvent) HandleSaleEvent(portfolioEvent as SaleEvent);
        }

        private void HandleSaleEvent(SaleEvent e)
        {
            if (!_assets.ContainsKey(e.Name)) _assets.Add(e.Name, new Asset(e.Name));
            _assets[e.Name].Sell(e.Date, e.TransactionDetail,
                e.TransactionTax, e.Quanity, e.SaleValue);
        }

        private void HandlePurchaseEvent(PurchaseEvent e)
        {
            if(!_assets.ContainsKey(e.Name)) _assets.Add(e.Name, new Asset(e.Name));
            _assets[e.Name].Purchase(e.Date, e.TransactionDetail,
                e.TransactionTax, e.Quanity, e.CostValue);
        }
    }
}
