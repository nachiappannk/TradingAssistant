using Prism.Commands;
using System;
using System.IO;
using Nachiappan.TradingAssistantViewModel.Model;
using Nachiappan.TradingAssistantViewModel.Model.Excel;

namespace Nachiappan.TradingAssistantViewModel
{
    public class InputWorkFlowStepViewModel : WorkFlowStepViewModel
    {
        private readonly DataStore _dataStore;
        private readonly Action _goToNextStep;

        public InputWorkFlowStepViewModel(DataStore dataStore, Action goToNextStep, Action goToPreviousStep)
        {
            _dataStore = dataStore;
            _goToNextStep = goToNextStep;
            Name = "Input";

            JournalSelectorViewModel = new ExcelSheetSelectorViewModel {Title = "Please provide the Trade Log"};
            //PreviousBalanceSheetSelectorViewModel =
            //    new ExcelSheetSelectorViewModel {Title = "Please provide the previous period balance sheet"};

            //AccountDefinitionViewModel = 
            //    new ExcelSheetSelectorViewModel() { Title = "Please provide the account definition"};


            JournalSelectorViewModel.ValidityChanged += RaiseCanExecuteChanged;
            //PreviousBalanceSheetSelectorViewModel.ValidityChanged += RaiseCanExecuteChanged;
            //AccountDefinitionViewModel.ValidityChanged += RaiseCanExecuteChanged;

            GoToPreviousCommand = new DelegateCommand(goToPreviousStep, () => true);
            GoToNextCommand = new DelegateCommand(GoToNext, CanGoToNext);

            SetParametersValueFromCache(dataStore);
        }

        private void SetParametersValueFromCache(DataStore dataStore)
        {
            if (dataStore.IsPackageStored(WorkFlowViewModel.InputParametersPackageDefinition))
            {
                var inputParameters = dataStore.GetPackage(WorkFlowViewModel.InputParametersPackageDefinition);

                SetExcelInputParameters(JournalSelectorViewModel,
                    inputParameters.TradeLogFileName, inputParameters.TradeLogSheetName);

                //SetExcelInputParameters(PreviousBalanceSheetSelectorViewModel,
                //    inputParameters.PreviousBalanceSheetFileName, inputParameters.PreviousBalanceSheetSheetName);

                //AccountingPeriodStartDate = inputParameters.AccountingPeriodStartDate;
                //AccountingPeriodEndDate = inputParameters.AccountingPeriodEndDate;
            }
        }

        private static void SetExcelInputParameters(ExcelSheetSelectorViewModel excelSelectorViewModel,
            string fileName,
            string sheetName)
        {
            if (File.Exists(fileName))
            {
                excelSelectorViewModel.InputFileName = fileName;
                if (excelSelectorViewModel.SheetNames.Contains(sheetName))
                {
                    excelSelectorViewModel.SelectedSheet = sheetName;
                }
            }
        }

        private void RaiseCanExecuteChanged()
        {
            GoToNextCommand.RaiseCanExecuteChanged();
        }
        
        public ExcelSheetSelectorViewModel JournalSelectorViewModel { get; set; }
        //public ExcelSheetSelectorViewModel PreviousBalanceSheetSelectorViewModel { get; set; }
        //public ExcelSheetSelectorViewModel AccountDefinitionViewModel { get; set; }

        //private DateTime? _accountingPeriodStartDate;
        //public DateTime? AccountingPeriodStartDate
        //{
        //    get => _accountingPeriodStartDate;
        //    set
        //    {
        //        if (_accountingPeriodStartDate != value)
        //        {
        //            _accountingPeriodStartDate = value;
        //            RaiseCanExecuteChanged();
        //        }
        //    }
        //}

        //private DateTime? _accountingPeriodEndDate;
        //public DateTime? AccountingPeriodEndDate
        //{
        //    get => _accountingPeriodEndDate;
        //    set
        //    {
        //        if (_accountingPeriodEndDate != value)
        //        {
        //            _accountingPeriodEndDate = value;
        //            RaiseCanExecuteChanged();
        //        }
        //    }
        //}

        bool CanGoToNext()
        {
            if (!JournalSelectorViewModel.IsValid) return false;
            //if (!PreviousBalanceSheetSelectorViewModel.IsValid) return false;
            //if (!AccountDefinitionViewModel.IsValid) return false;
            //if (AccountingPeriodEndDate == null) return false;
            //if (AccountingPeriodStartDate == null) return false;
            return true;
        }

        void GoToNext()
        {

            var input = new InputForTradeStatementComputation
            {
                // ReSharper disable once PossibleInvalidOperationException
                //AccountingPeriodEndDate = AccountingPeriodEndDate.Value,
                //// ReSharper disable once PossibleInvalidOperationException
                //AccountingPeriodStartDate = AccountingPeriodStartDate.Value,
                //PreviousBalanceSheetFileName = PreviousBalanceSheetSelectorViewModel.InputFileName,
                //PreviousBalanceSheetSheetName = PreviousBalanceSheetSelectorViewModel.SelectedSheet,
                TradeLogFileName = JournalSelectorViewModel.InputFileName,
                TradeLogSheetName = JournalSelectorViewModel.SelectedSheet,
                //AccountDefinitionFileName = AccountDefinitionViewModel.InputFileName,
                //AccountDefintionSheetName = AccountDefinitionViewModel.SelectedSheet,
            };

            _dataStore.PutPackage(input ,WorkFlowViewModel.InputParametersPackageDefinition);
            _goToNextStep.Invoke();
        }
    }
}