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

    public class StatementsGenerator
    {
        Dictionary<string, string> _displayNames = new Dictionary<string, string>();

        public StatementsGenerator(List<RecordedTradeEvent> statements)
        {
            InitializeDisplayNames(statements);
        }

        private void InitializeDisplayNames(List<RecordedTradeEvent> statements)
        {
            _displayNames = new Dictionary<string, string>();
            foreach (var tradeStatement in statements)
            {
                var name = tradeStatement.Name;
                var key = name.ToLower();
                if (!_displayNames.ContainsKey(key)) _displayNames.Add(key, name);
            }
        }
    }

    public class Account
    {
        public string Name { get; set; }
        private List<GainStatement> GainStatements { get; set; }
        private List<RecordedTradeEvent> HoldingStatements { get; set; }

        public Account()
        {
            GainStatements = new List<GainStatement>();
            HoldingStatements = new List<RecordedTradeEvent>();
        }

        public void AddStatement(RecordedTradeEvent recordedTradeEvent)
        {
            if (recordedTradeEvent.Value > 0)
            {

            }
        }

        public List<HoldingStatement> Close(string periodName, double closingValue)
        {
            return new List<HoldingStatement>();
        }


    }

    public class HoldingStatement
    {
        public string Name { get; set; }
        public string PeriodName { get; set; }
        public double Quanity { get; set; }
        public DateTime Date { get; set; }
        public string TransactionDetail { get; set; }
        public string TransactionTax { get; set; }
        public double Value { get; set; }
        public double EffectiveCost { get; set; }
        public double ClosingCost { get; set; }
        public string Remark { get; set; }
    }

    public class GainStatement
    {
        public string Name { get; set; }
        public double Quanity { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string PurchaseTransactionDetail { get; set; }
        public string PurchaseTransactionTax { get; set; }
        public double PurchaseValue { get; set; }
        public DateTime SaleDate { get; set; }
        public string SaleTransactionDetail { get; set; }
        public string SaleTransactionTax { get; set; }
        public double SaleValue { get; set; }
    }
}