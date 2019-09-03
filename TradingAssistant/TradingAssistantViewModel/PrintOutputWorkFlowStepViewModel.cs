using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Documents;
using Nachiappan.TradingAssistantViewModel.Model;
using Nachiappan.TradingAssistantViewModel.Model.Account;
using Nachiappan.TradingAssistantViewModel.Model.Excel;
using Nachiappan.TradingAssistantViewModel.Model.ExcelGateway;
using Nachiappan.TradingAssistantViewModel.Model.Statements;
using Nachiappan.TradingAssistantViewModel.StatementDisplayingViewModel;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;

namespace Nachiappan.TradingAssistantViewModel
{
    public class PrintOutputWorkFlowStepViewModel : WorkFlowStepViewModel
    {
        public List<DisplayableJournalStatement> JournalStatements { get; set; }
        
        private readonly DataStore _dataStore;
        public DelegateCommand SaveOutputCommand { get; set; }
        
        public InteractionRequest<FileSaveAsNotification> SaveOutputRequest { get; private set; }

        public PrintOutputWorkFlowStepViewModel(DataStore dataStore, Action goToPrevious)
        {
            _dataStore = dataStore;
            Name = "Save Output";
            GoToPreviousCommand = new DelegateCommand(goToPrevious);
            GoToNextCommand = new DelegateCommand(() => { }, ()=>false);
            SaveOutputCommand = new DelegateCommand(SaveOutput);
            SaveOutputRequest = new InteractionRequest<FileSaveAsNotification>();
            
            JournalStatements = new List<DisplayableJournalStatement>();
            JournalStatements.Add(new DisplayableJournalStatement()
            {
                Account = "Test",
                Credit = 4.4,
                Date = new DateTime(2019,03,03),
                Debit = 56.7,
                Description = "SomeThing",
                Tag = "My Tag",
            });
            
            
        }

        private void SaveOutput()
        {
            SaveFile("Save Output Document",
                "FinancialReport",
                ".xlsx",
                "Excel File (.xlsx)|*.xlsx");
        }

        private void SaveFile(string saveFileTitle, string defaultFileName, string outputFileExtention, string filter)
        {
            var file = new FileSaveAsNotification()
            {
                Title = saveFileTitle,
                DefaultFileName = defaultFileName,
                OutputFileExtention = outputFileExtention,
                OutputFileExtentionFilter = filter,
            };

            SaveOutputRequest.Raise(file);
            if (file.FileSaved)
            {
                var outputFileName = file.OutputFileName;
                if (File.Exists(outputFileName)) File.Delete(outputFileName);
                WriteJournal(outputFileName);
            }
        }

        private void WriteJournal(String fileName)
        {
            var journalStatementsa = JournalStatements.Select(x => new JournalStatement()
            {
                Account = "acc",
                Date = new DateTime(201, 3, 4),
                Description = "dfasf",
                Tag = "t",
                Value = -44
            }).ToList();
            JournalGateway gateway = new JournalGateway(fileName);
            gateway.WriteJournal(journalStatementsa);
        }       
    }
}