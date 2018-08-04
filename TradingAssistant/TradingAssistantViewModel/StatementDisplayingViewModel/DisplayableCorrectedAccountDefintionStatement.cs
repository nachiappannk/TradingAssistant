using System.ComponentModel;
using Nachiappan.TradingAssistantViewModel.Model.Statements;

namespace Nachiappan.TradingAssistantViewModel.StatementDisplayingViewModel
{
    public class DisplayableCorrectedAccountDefintionStatement
    {
        public DisplayableCorrectedAccountDefintionStatement(CorrectedAccountDefintionStatement x)
        {
            AccountType = x.AccountType;
            Account = x.Account;
            RecipientAccount = x.RecipientAccount;
            Reason = x.Reason;
        }

        [DisplayName("Account Type")]
        public AccountType AccountType { get; set; }

        public string Account { get; set; }

        [DisplayName("Recipient Account")]
        public string RecipientAccount { get; set; }
        public string Reason { get; set; }
    }
}