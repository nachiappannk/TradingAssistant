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

            TradeLogSelectorViewModel = new ExcelSheetSelectorViewModel { Title = "Please provide the Trade Log" };

            OpenPositionsSelectorViewModel = new ExcelSheetSelectorViewModel { Title = "Please provide the Open position" };

            CashLogSelectorViewModel = new ExcelSheetSelectorViewModel { Title = "Please provide the Cash log" };

            TradeLogSelectorViewModel.ValidityChanged += RaiseCanExecuteChanged;
            OpenPositionsSelectorViewModel.ValidityChanged += RaiseCanExecuteChanged;
            CashLogSelectorViewModel.ValidityChanged += RaiseCanExecuteChanged;

            GoToPreviousCommand = new DelegateCommand(goToPreviousStep, () => true);
            GoToNextCommand = new DelegateCommand(GoToNext, CanGoToNext);

            SetParametersValueFromCache(dataStore);
        }

        private void SetParametersValueFromCache(DataStore dataStore)
        {
            if (dataStore.IsPackageStored(WorkFlowViewModel.InputParametersPackageDefinition))
            {
                var inputParameters = dataStore.GetPackage(WorkFlowViewModel.InputParametersPackageDefinition);

                SetExcelInputParameters(TradeLogSelectorViewModel,
                    inputParameters.TradeLogFileName, inputParameters.TradeLogSheetName);

                IsPeriodAccounting = inputParameters.IsPeriodAccounting;
                if(IsPeriodAccounting)
                { 
                    SetExcelInputParameters(OpenPositionsSelectorViewModel,
                        inputParameters.TradeLogFileName, inputParameters.TradeLogSheetName);

                    SetExcelInputParameters(CashLogSelectorViewModel,
                        inputParameters.CashLogFileName, inputParameters.CashLogSheetName);
                    if (inputParameters.AccountingPeriodStartDate.HasValue)
                    {
                        AccountingPeriodStartDate = inputParameters.AccountingPeriodStartDate.Value;
                    }

                    if (inputParameters.AccountingPeriodEndDate.HasValue)
                    {
                        AccountingPeriodEndDate = inputParameters.AccountingPeriodEndDate.Value;
                    }

                }

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

        public ExcelSheetSelectorViewModel TradeLogSelectorViewModel { get; set; }

        public ExcelSheetSelectorViewModel OpenPositionsSelectorViewModel { get; set; }

        public ExcelSheetSelectorViewModel CashLogSelectorViewModel { get; set; }

        private DateTime _accountingPeriodStartDate;
        public DateTime AccountingPeriodStartDate
        {
            get => _accountingPeriodStartDate;
            set
            {
                if (_accountingPeriodStartDate != value)
                {
                    _accountingPeriodStartDate = value;
                    FirePropertyChanged();
                    RaiseCanExecuteChanged();
                }
            }
        }

        private bool _isPeriodAccounting;

        public bool IsPeriodAccounting
        {
            get => _isPeriodAccounting;
            set
            {
                if (_isPeriodAccounting != value)
                {
                    _isPeriodAccounting = value;
                    FirePropertyChanged();
                    RaiseCanExecuteChanged();
                }

            }
        }


        private bool _isTraingAccountLedgerNeeded;

        public bool IsTraingAccountLedgerNeeded
        {
            get => _isTraingAccountLedgerNeeded;
            set
            {
                if (_isTraingAccountLedgerNeeded != value)
                {
                    _isTraingAccountLedgerNeeded = value;
                    FirePropertyChanged();
                    RaiseCanExecuteChanged();
                }

            }
        }


        private DateTime _accountingPeriodEndDate;
        

        public DateTime AccountingPeriodEndDate
        {
            get => _accountingPeriodEndDate;
            set
            {
                if (_accountingPeriodEndDate != value)
                {
                    _accountingPeriodEndDate = value;
                    RaiseCanExecuteChanged();
                    FirePropertyChanged();
                }
            }
        }

        bool CanGoToNext()
        {
            if (!TradeLogSelectorViewModel.IsValid) return false;
            if (!IsPeriodAccounting) return true;

            if (!OpenPositionsSelectorViewModel.IsValid) return false;
            //if (AccountingPeriodEndDate == null) return false;
            //if (AccountingPeriodStartDate == null) return false;
            if (!IsTraingAccountLedgerNeeded) return true;

            if (!CashLogSelectorViewModel.IsValid) return false;
            return true;
        }

        void GoToNext()
        {

            var input = new InputForTradeStatementComputation
            {
                TradeLogFileName = TradeLogSelectorViewModel.InputFileName,
                TradeLogSheetName = TradeLogSelectorViewModel.SelectedSheet,
                IsPeriodAccounting = IsPeriodAccounting,
                CashLogSheetName = IsPeriodAccounting? CashLogSelectorViewModel.SelectedSheet : string.Empty,
                CashLogFileName = IsPeriodAccounting ? CashLogSelectorViewModel.InputFileName : string.Empty,
                OpenPositionFileName = IsPeriodAccounting ? OpenPositionsSelectorViewModel.InputFileName : string.Empty,
                OpenPositionSheetName = IsPeriodAccounting ? OpenPositionsSelectorViewModel.SelectedSheet : string.Empty,
                AccountingPeriodEndDate = AccountingPeriodEndDate,
                AccountingPeriodStartDate = AccountingPeriodStartDate,
            };

            _dataStore.PutPackage(input, WorkFlowViewModel.InputParametersPackageDefinition);
            _goToNextStep.Invoke();
        }
    }
}