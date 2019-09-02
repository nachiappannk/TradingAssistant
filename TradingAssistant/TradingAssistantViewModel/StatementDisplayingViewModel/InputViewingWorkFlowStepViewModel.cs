using System;
using System.Collections.Generic;
using System.Linq;
using Nachiappan.TradingAssistantViewModel.Model;
using Nachiappan.TradingAssistantViewModel.Model.Statements;
using Prism.Commands;

namespace Nachiappan.TradingAssistantViewModel.StatementDisplayingViewModel
{
    public class InputViewingWorkFlowStepViewModel : WorkFlowStepViewModel
    {
        public List<DisplayableJournalStatement> JournalStatements { get; set; }
        public List<DisplayableTrimmedJournalStatement> TrimmedJournalStatements { get; set; }

        public InputViewingWorkFlowStepViewModel(DataStore dataStore, Action goBackAction)
        {
            GoToPreviousCommand = new DelegateCommand(goBackAction);
            GoToNextCommand = new DelegateCommand(() => { }, () => false);
            Name = "View Input Statements";

            //var journalStatements = 
            //    dataStore.GetPackage(WorkFlowViewModel.InputJournalStatementsPackageDefintion)
            //        .MakeAccountPrintable(dataStore,
            //            WorkFlowViewModel.DisplayableAccountNamesDictionaryPackageDefinition);
            //;
            //JournalStatements = journalStatements.Select(x => new DisplayableJournalStatement(x)).ToList();

            //var correctedJournalStatements =
            //    dataStore.GetPackage(WorkFlowViewModel.TrimmedJournalStatementsPackageDefintion)
            //        .MakeAccountPrintable(dataStore,
            //            WorkFlowViewModel.DisplayableAccountNamesDictionaryPackageDefinition);
            //;

            //TrimmedJournalStatements = correctedJournalStatements.Select(x => new DisplayableTrimmedJournalStatement(x))
            //    .ToList();
        }
    }
}