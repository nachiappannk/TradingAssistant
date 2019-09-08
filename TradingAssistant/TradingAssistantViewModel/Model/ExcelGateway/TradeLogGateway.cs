﻿using System;
using System.Collections.Generic;
using System.Linq;
using Nachiappan.TradingAssistantViewModel.Model.Excel;
using Nachiappan.TradingAssistantViewModel.Model.Statements;

namespace Nachiappan.TradingAssistantViewModel.Model.ExcelGateway
{
    public class TradeLogGateway
    {
        private readonly string _inputFile;

        private List<string> _headings = new List<string>()
        {
            "S.No.", "Date",  "Name", "Transaction Detail",  "Transaction Tax", "Quantity" , "Cost", "Sale"
        };

        const int SerialNumber = 0;
        const int Date = 1;
        const int Name = 2;
        const int TransactionDetail = 3;
        const int TransactionTax = 4;
        const int Quantity = 5;
        const int Cost = 6;
        const int Sale = 7;

        public TradeLogGateway(string inputFile)
        {
            _inputFile = inputFile;
        }

        public void WriteTradeLog(IList<RecordedTradeEvent> tradeStatement)
        {
            using (ExcelSheetWriter writer = new ExcelSheetWriter(_inputFile, "TradeLog"))
            {
                int index = 0;

                writer.Write(index++, _headings.ToArray());
                writer.SetColumnsWidth(6, 12, 35, 30, 30, 12, 12, 12);
                writer.ApplyHeadingFormat(_headings.Count);
                writer.WriteList(index, tradeStatement.OrderBy(x => x.Date).ToList(),
                    (j, rowIndex) => new object[]
                    {
                        rowIndex - 1,
                        j.Date,
                        j.Name,
                        j.TransactionDetail,
                        j.TransactionTax,
                        //j.(),
                        //j.GetDebitValueOrNull(),
                    });

            }
        }

        public List<RecordedTradeEvent> GetTradeStatements(ILogger logger, string sheetName)
        {
            using (ExcelReader reader = new ExcelReader(_inputFile, sheetName, logger))
            {
                SheetHeadingVerifier.VerifyHeadingNames(logger, reader, _headings);

                var tradeStatements = reader.ReadAllLines(1, (r) =>
                {
                    if (!r.IsValueAvailable(SerialNumber)) return Tuple.Create(new RecordedTradeEvent(), false);
                    var tradeStatement = new RecordedTradeEvent()
                    {
                        SerialNumberString = r.ReadString(SerialNumber),
                        SerialNumber = r.ReadInteger(SerialNumber),
                        Date = r.ReadDate(Date),
                        Name = r.ReadString(Name),
                        TransactionTax = r.ReadString(TransactionTax),
                        TransactionDetail = r.ReadString(TransactionDetail),
                        Quanity = r.ReadDouble(Quantity),
                        CostValue = r.ReadDoubleIfAvailable(Cost),
                        SaleValue = r.ReadDoubleIfAvailable(Sale),
                    };
                    return Tuple.Create(tradeStatement, true);
                }).ToList();
                return tradeStatements.Where(x => x.Item2).Select(x => x.Item1).ToList();
            }
        }
    }
}