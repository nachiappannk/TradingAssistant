using System.Collections.Generic;
using System.Text.RegularExpressions;
using Nachiappan.TradingAssistantViewModel.Model.Statements;

namespace Nachiappan.TradingAssistantViewModel.Model.ExcelGateway
{
    public class BalanceSheetReader
    {
        public static List<BalanceSheetStatement> ReadBalanceSheetStatements(string fileName, string sheetName, ILogger logger)
        {
            BalanceSheetGateway balanceSheetGateway = new BalanceSheetGateway(fileName);
            var balanceSheetStatements = balanceSheetGateway.GetBalanceSheet(logger, sheetName);
            TrimBalanceSheetAccount(balanceSheetStatements);
            return balanceSheetStatements;
        }

        private static void TrimBalanceSheetAccount(List<BalanceSheetStatement> balanceSheetStatements)
        {
            balanceSheetStatements.ForEach(x =>
            {
                if (string.IsNullOrWhiteSpace(x.Account))
                {
                    x.Account = string.Empty;
                }

                x.Account = x.Account.ToLower();
                x.Account = Regex.Replace(x.Account, @"\s+", " ");
                x.Account = x.Account.Trim();
            });
        }
    }
}