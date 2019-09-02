using Nachiappan.TradingAssistantViewModel.Model.Statements;

namespace Nachiappan.TradingAssistantViewModel
{
    public abstract class Information
    {
        public string Message { get; set; }

        public static Information CreateError(string message)
        {
            return new Error() { Message = message };
        }

        public static Information CreateWarning(string message)
        {
            return new Warning() { Message = message };
        }
    }


    public class Warning : Information
    {
    }

    public class Error : Information
    {
    }
}