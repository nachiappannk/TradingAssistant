﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Nachiappan.TradingAssistantViewModel.Model;
using Nachiappan.TradingAssistantViewModel.Model.Account;
using Nachiappan.TradingAssistantViewModel.Model.ExcelGateway;
using Nachiappan.TradingAssistantViewModel.Model.Statements;
using Prism.Commands;

namespace Nachiappan.TradingAssistantViewModel.StatementDisplayingViewModel
{
    public class StatementReadingAndVerifyingWorkFlowStepViewModel : WorkFlowStepViewModel
    {
        private readonly DataStore _dataStore;
        private List<DisplayableCleanedTradeEvent> _displayableCorrectedTradeStatements;

        public List<DisplayableCleanedTradeEvent> DisplayableCorrectedTradeStatements
        {
            get => _displayableCorrectedTradeStatements;
            set
            {
                _displayableCorrectedTradeStatements = value;
                FirePropertyChanged();
            }
        }

        public DelegateCommand ReadAgainCommand { get; set; }

        public StatementReadingAndVerifyingWorkFlowStepViewModel(DataStore dataStore, Action goToPreviousStep, 
            Action goToNextStep)
        {
            _dataStore = dataStore;


            GoToPreviousCommand = new DelegateCommand(goToPreviousStep);
            GoToNextCommand = new DelegateCommand(goToNextStep);
            ReadAgainCommand = new DelegateCommand(ReadInput);

            Name = "Read And Verify Input";
            ReadInput();
        }

        private void ReadInput()
        {
            var input = _dataStore.GetPackage(WorkFlowViewModel.InputParametersPackageDefinition);

            var logger = new Logger();

            var gateway = new TradeLogGateway(input.TradeLogFileName);

            var recordedTradeEvents = gateway.GetTradeStatements(logger, input.TradeLogSheetName);

            var cleanedTradeEvents = recordedTradeEvents.Select(x => new CleanedTradeEvent(x)).ToList();
            DisplayableCorrectedTradeStatements = cleanedTradeEvents.Select(x => new DisplayableCleanedTradeEvent(x)).ToList();
        }
    }
}