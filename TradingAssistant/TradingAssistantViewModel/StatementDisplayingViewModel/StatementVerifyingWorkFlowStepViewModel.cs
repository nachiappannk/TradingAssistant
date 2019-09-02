using System;
using System.Collections.Generic;
using System.Linq;
using Nachiappan.TradingAssistantViewModel.Model;
using Nachiappan.TradingAssistantViewModel.Model.Account;
using Nachiappan.TradingAssistantViewModel.Model.Statements;
using Prism.Commands;

namespace Nachiappan.TradingAssistantViewModel.StatementDisplayingViewModel
{
    public class StatementVerifyingWorkFlowStepViewModel : WorkFlowStepViewModel
    {
        public List<DisplayableCorrectedTradeStatement> DisplayableCorrectedTradeStatements { get; set; }

        public StatementVerifyingWorkFlowStepViewModel(DataStore dataStore, Action goToPreviousStep, 
            Action goToNextStep)
        {
            var tradeStatements = dataStore.GetPackage(WorkFlowViewModel.InputTradeStatementPackageDefinition);
            DisplayableCorrectedTradeStatements = tradeStatements.Select(x => new DisplayableCorrectedTradeStatement(x)).ToList();
            
            GoToPreviousCommand = new DelegateCommand(goToPreviousStep);
            GoToNextCommand = new DelegateCommand(goToNextStep);
            Name = "Verify Input/Output Statements";
        }   
    }
}