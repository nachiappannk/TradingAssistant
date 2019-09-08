using System;

namespace Nachiappan.TradingAssistantViewModel.Model.Statements
{
    public class JournalStatement
    {
        public string Account { get; set; }
        public DateTime Date { get; set; }
        public double Value { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }
    }
}