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

            TradeLogSelectorViewModel.ValidityChanged += RaiseCanExecuteChanged;

            GoToPreviousCommand = new DelegateCommand(goToPreviousStep, () => true);
            GoToNextCommand = new DelegateCommand(GoToNext, CanGoToNext);
            
        }
        
        private void RaiseCanExecuteChanged()
        {
            GoToNextCommand.RaiseCanExecuteChanged();
        }

        public ExcelSheetSelectorViewModel TradeLogSelectorViewModel { get; set; }

        bool CanGoToNext()
        {
            if (!TradeLogSelectorViewModel.IsValid) return false;
            return true;
        }

        void GoToNext()
        {

            var input = new InputForTradeStatementComputation
            {
                TradeLogFileName = TradeLogSelectorViewModel.InputFileName,
                TradeLogSheetName = TradeLogSelectorViewModel.SelectedSheet,
            };

            _dataStore.PutPackage(input, WorkFlowViewModel.InputParametersPackageDefinition);
            _goToNextStep.Invoke();
        }
    }
}