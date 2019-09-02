using System.Collections.Generic;
using Nachiappan.TradingAssistantViewModel.Model;

namespace Nachiappan.TradingAssistantViewModel
{
    public class Logger : ILogger
    {

        public List<Information> InformationList = new List<Information>();
        public void Log(MessageType type, string message)
        {
            if (type == MessageType.Warning)
                InformationList.Add(Information.CreateWarning(message));

            if (type == MessageType.Error)
                InformationList.Add(Information.CreateError(message));
        }
    }
}