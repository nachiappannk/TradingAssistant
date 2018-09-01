using System;

namespace Nachiappan.TradingAssistantViewModel
{
    public class InputForTradeStatementComputation
    {
        public string OpenPositionFileName { get; set; }
        public string OpenPositionSheetName { get; set; }
        public string TradeLogFileName { get; set; }
        public string TradeLogSheetName { get; set; }
        public string CashLogFileName { get; set; }
        public string CashLogSheetName { get; set; }
        public DateTime? AccountingPeriodStartDate { get; set; }
        public DateTime? AccountingPeriodEndDate { get; set; }
        public bool IsPeriodAccounting { get; set; }
        public bool IsTradingAccountLedgerNeeded { get; set; }
    }
}