﻿using System;

namespace Nachiappan.TradingAssistantViewModel
{
    public class InputForBalanceSheetComputation
    {
        public string PreviousBalanceSheetFileName { get; set; }
        public string CurrentJournalFileName { get; set; }
        public string PreviousBalanceSheetSheetName { get; set; }
        public string CurrentJournalSheetName { get; set; }
        public string AccountDefinitionFileName { get; set; }
        public string AccountDefintionSheetName { get; set; }
        public DateTime AccountingPeriodStartDate { get; set; }
        public DateTime AccountingPeriodEndDate { get; set; }

    }
}