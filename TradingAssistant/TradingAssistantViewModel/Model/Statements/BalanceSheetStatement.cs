namespace Nachiappan.TradingAssistantViewModel.Model.Statements
{
    public class BalanceSheetStatement : IHasValue, IHasAccount, ICanClone<BalanceSheetStatement>
    {
        public string Account { get; set; }
        public double Value { get; set; }
        public BalanceSheetStatement Clone()
        {
            return new BalanceSheetStatement()
            {
                Account = Account,
                Value = Value,
            };
        }
    }
}