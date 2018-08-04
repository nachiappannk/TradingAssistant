namespace Nachiappan.TradingAssistantViewModel.Model.Statements
{
    public interface ICanClone<out T>
    {
        T Clone();
    }
}