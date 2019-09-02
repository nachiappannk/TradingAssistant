using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
            tradeStatements = tradeStatements.Select(x => TradeStatementAdjuster.Adjust(x)).ToList();
            DisplayableCorrectedTradeStatements = tradeStatements.Select(x => new DisplayableCorrectedTradeStatement(x)).ToList();
            
            GoToPreviousCommand = new DelegateCommand(goToPreviousStep);
            GoToNextCommand = new DelegateCommand(goToNextStep);
            Name = "Verify Input/Output Statements";
        }   
    }

    public class TradeStatementAdjuster
    {
        public static AdjustedTradeStatement Adjust(AdjustedTradeStatement st)
        {
            if (st.Name.Contains("##"))
            {
                if (st.Name.ToLower().StartsWith("##end##"))
                {

                }
                else if (st.Name.ToLower().Contains("##split##"))
                {

                }
                else
                {
                    st.AddReason("The command is not understood.");
                }
            }
            else
            {
                AdjustName(st);
                AdjustValue(st);
                AdjustQuantity(st);
            }
            return st;
        }

        private static void AdjustQuantity(AdjustedTradeStatement st)
        {
            if (st.Quanity.IsZero()) st.AddReason("Quantity is not mentioned as zero.");
        }

        private static void AdjustValue(AdjustedTradeStatement st)
        {
            if (st.Value.IsZero()) st.AddReason("Both cost and sale is zero. Please verify if this is a bonus Transaction.");
        }

        private static void AdjustName(AdjustedTradeStatement st)
        {
            var name = st.Name;
            st.Name = GetCorrectedName(name);
            if (st.Name != name) st.AddReason($"'The input {name}' is adjusted as $'{st.Name}'.");
        }

        private static string GetCorrectedName(string s)
        {
            var input = s;
            var output = input;
            output = output.Trim();
            output = Regex.Replace(output, @"\s+", " ");
            output = Regex.Replace(output, @"[^0-9a-zA-Z&#\s]+", "");
            if (output.Contains("##")) output = Regex.Replace(output, @"\s", "");
            var parts = output.Split(' ').ToList();
            parts = parts.Select(x => x.ToLower()).ToList();
            parts = parts.Select(x => {
                if (x.Length > 0)
                {
                    char[] letters = x.ToCharArray();
                    letters[0] = char.ToUpper(letters[0]);
                    return new string(letters);
                }
                return x;
            }).ToList();
            output = string.Join(" ", parts);
            return output;
        }
    }
}