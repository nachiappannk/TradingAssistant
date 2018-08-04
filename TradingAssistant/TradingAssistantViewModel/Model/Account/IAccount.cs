using System;
using System.Collections.Generic;
using Nachiappan.TradingAssistantViewModel.Model.Statements;

namespace Nachiappan.TradingAssistantViewModel.Model.Account
{
    public interface IAccount
    {
        string GetName();
        string GetPrintableName();
        void PostStatement(DateTime date, string statement, double value);
        double GetAccountValue();
        List<AccountStatement> GetAccountStatements();
        AccountType GetAccountType();
    }
}