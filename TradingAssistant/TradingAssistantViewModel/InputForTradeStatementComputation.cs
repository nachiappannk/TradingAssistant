using System;

namespace Nachiappan.TradingAssistantViewModel
{
    public class InputForTradeStatementComputation
    {
        public string PreviousBalanceSheetFileName { get; set; }
        public string TradeLogFileName { get; set; }
        public string PreviousBalanceSheetSheetName { get; set; }
        public string TradeLogSheetName { get; set; }
        public string AccountDefinitionFileName { get; set; }
        public string AccountDefintionSheetName { get; set; }
        public DateTime AccountingPeriodStartDate { get; set; }
        public DateTime AccountingPeriodEndDate { get; set; }

    }
}