using System;
using System.Collections.Generic;
using System.Linq;
using Nachiappan.TradingAssistantViewModel.Model;
using Nachiappan.TradingAssistantViewModel.Model.ExcelGateway;
using Nachiappan.TradingAssistantViewModel.Model.Statements;
using Nachiappan.TradingAssistantViewModel.StatementDisplayingViewModel;
using Prism.Commands;

namespace Nachiappan.TradingAssistantViewModel
{
    public class InputReadingWorkFlowStepViewModel : WorkFlowStepViewModel
    {
        public DelegateCommand ReadAgainCommand { get; set; }

        public DelegateCommand ViewInputStatementsCommand { get; set; }

        private readonly DataStore _dataStore;
        private readonly Action<WorkFlowStepViewModel> _setCurrentStep;

        public List<Information> InformationList
        {
            get { return _informationList; }
            set
            {
                _informationList = value;
                FirePropertyChanged();
            }
        }

        private List<Information> _informationList;
        private string _overAllMessage;
        

        public string OverAllMessage
        {
            get { return _overAllMessage; }
            set
            {
                _overAllMessage = value;
                FirePropertyChanged();
            }
        }

        public InputReadingWorkFlowStepViewModel(DataStore dataStore, Action goToInputStep,
            Action goToStatementVerifyingWorkFlowStep, Action<WorkFlowStepViewModel> setCurrentStep)
        {
            _dataStore = dataStore;
            _setCurrentStep = setCurrentStep;
            Name = "Read Input";
            GoToPreviousCommand = new DelegateCommand(goToInputStep);
            GoToNextCommand = new DelegateCommand(goToStatementVerifyingWorkFlowStep);
            ReadAgainCommand = new DelegateCommand(ReadInput);
            ViewInputStatementsCommand = new DelegateCommand(GoToViewStatements);
            ReadInput();
        }

        private void GoToViewStatements()
        {
            _setCurrentStep.Invoke(new InputViewingWorkFlowStepViewModel(_dataStore,
                () => _setCurrentStep.Invoke(this)));
        }

        private void ReadInput()
        {
            var input = _dataStore.GetPackage(WorkFlowViewModel.InputParametersPackageDefinition);

            var logger = new Logger();

            var gateway = new TradeLogGateway(input.TradeLogFileName);

            var tradeStatements = gateway.GetTradeStatements
                (logger, input.TradeLogSheetName);


            _dataStore.PutPackage(tradeStatements, WorkFlowViewModel.InputTradeStatementPackageDefinition);

            logger.InformationList.Sort((a, b) =>
            {
                if (a.GetType() == b.GetType()) return 0;
                if (a.GetType() == typeof(Error)) return -1;
                return 1;
            });

            InformationList = logger.InformationList.ToList();
            OverAllMessage = GetOverAllErrorMessage(logger.InformationList.ToList());

        }

        private static string GetOverAllErrorMessage(List<Information> errorsAndWarnings)
        {
            var errorCount = errorsAndWarnings.Count(x => x.GetType() == typeof(Error));
            var warningCount = errorsAndWarnings.Count(x => x.GetType() == typeof(Warning));

            if (errorCount > 1 && warningCount > 1) return "Please check inputs. There are few errors and warnings";
            if (errorCount > 1 && warningCount == 1) return "Please check inputs. There are few errors and one warning";
            if (errorCount > 1 && warningCount == 0) return "Please check inputs. There are few errors";
            if (errorCount == 1 && warningCount > 1) return "Please check inputs. There is an errors and a few warnings";
            if (errorCount == 1 && warningCount == 1) return "Please check inputs. There is an errors and a warning";
            if (errorCount == 1 && warningCount == 0) return "Please check inputs. There is an errors";
            if (errorCount == 0 && warningCount > 1) return "Please check inputs. There are few warnings";
            if (errorCount == 0 && warningCount == 1) return "Please check inputs. There is a warning";
            return "Congrats!!! There are no errors or warnings. Please verify output";
        }
    }


    public class Logger : ILogger
    {

        public List<Information> InformationList = new List<Information>();
        public void Log(MessageType type, string message)
        {
            if (type == MessageType.Warning)
                InformationList.Add(Information.CreateWarning(message));

            if (type == MessageType.Error)
                InformationList.Add(Information.CreateError(message));
        }
    }


    public abstract class Information
    {
        public string Message { get; set; }

        public static Information CreateError(string message)
        {
            return new Error() { Message = message };
        }

        public static Information CreateWarning(string message)
        {
            return new Warning() { Message = message };
        }
    }


    public class Warning : Information
    {
    }

    public class Error : Information
    {
    }
}