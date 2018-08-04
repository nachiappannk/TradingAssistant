﻿using System;
using System.Collections.Generic;
using System.Linq;
using Nachiappan.TradingAssistantViewModel.Model.Account;
using Nachiappan.TradingAssistantViewModel.Model.Statements;

namespace Nachiappan.TradingAssistantViewModel.Model.ExcelGateway
{
    public class JournalStatementsCorrecter
    {
        public static List<CorrectedJournalStatement> CorrectInvalidStatements(
            List<JournalStatement> journalStatements, DateTime startDate, DateTime endDate,
            Logger logger)
        {
            var tJournalStatements = RemoveOutOfDateStatements(journalStatements, startDate, endDate);
            tJournalStatements.AddRange(RemoveStatementsWithInvalidAccount(journalStatements));
            tJournalStatements.AddRange(RemoveStatementsWithInvalidDescription(journalStatements));
            ValidateTrimmedJournalStatements(tJournalStatements, logger);
            return tJournalStatements;
        }

        private static IEnumerable<CorrectedJournalStatement> RemoveStatementsWithInvalidDescription(
            List<JournalStatement> statements)
        {
            var filteredStatements2 = statements.Where(x => string.IsNullOrWhiteSpace(x.Description)).ToList();
            statements.RemoveAll(x => filteredStatements2.Contains(x));
            var trimmedJournalStatements =
                filteredStatements2.Select(x => new CorrectedJournalStatement(x, "The description is invalid"));
            return trimmedJournalStatements;
        }

        private static List<CorrectedJournalStatement> RemoveStatementsWithInvalidAccount(
            List<JournalStatement> statements)
        {
            var invalidAccountStatement = statements.Where(x =>
            {
                return !AccountNameValidator.IsAccountNameValid(x.Account);
            }).ToList();

            statements.RemoveAll(x => invalidAccountStatement.Contains(x));
            return invalidAccountStatement
                .Select(x => new CorrectedJournalStatement(x, "The account is invalid")).ToList();
        }

        private static void ValidateTrimmedJournalStatements
            (List<CorrectedJournalStatement> trimmedStatements, ILogger logger)
        {
            if (trimmedStatements.Any())
            {
                logger.Log(MessageType.Warning,
                    "The invalid journal statement(s) have been removed.");
            }
        }

        private static List<CorrectedJournalStatement> RemoveOutOfDateStatements(List<JournalStatement> statements,
            DateTime startDate, DateTime endDate)
        {
            var statementsBeforePeriod = statements.Where(x => x.Date < startDate).ToList();
            statements.RemoveAll(x => statementsBeforePeriod.Contains(x));
            var statementsAfterPeriod = statements.Where(x => x.Date > endDate).ToList();
            statements.RemoveAll(x => statementsAfterPeriod.Contains(x));

            var trimmedStatements = statementsAfterPeriod
                .Select(x => new CorrectedJournalStatement(x, "After the end of  accounting period")).ToList();
            trimmedStatements.AddRange(statementsBeforePeriod.Select(x =>
                new CorrectedJournalStatement(x, "Before the start of accounting period")));

            return trimmedStatements;
        }
    }
}