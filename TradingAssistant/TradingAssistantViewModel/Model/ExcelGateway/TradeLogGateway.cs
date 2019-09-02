using System;
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

        public void WriteTradeLog(IList<TradeStatement> tradeStatement)
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
                        j.GetCreditValueOrNull(),
                        j.GetDebitValueOrNull(),
                    });

            }
        }

        public List<AdjustedTradeStatement> GetTradeStatements(ILogger logger, string sheetName)
        {
            using (ExcelReader reader = new ExcelReader(_inputFile, sheetName, logger))
            {
                SheetHeadingVerifier.VerifyHeadingNames(logger, reader, _headings);

                var tradeStatements = reader.ReadAllLines(1, (r) =>
                {
                    if (!r.IsValueAvailable(SerialNumber)) return Tuple.Create(new AdjustedTradeStatement(), false);
                    var isSaleAvailable = r.IsValueAvailable(Sale);
                    var sale = isSaleAvailable ? r.ReadDouble(Sale) : 0;
                    var isCostAvailable = r.IsValueAvailable(Cost);
                    var cost = isCostAvailable ? r.ReadDouble(Cost) : 0;
                    var reason = string.Empty;
                    var value = sale - cost;
                    if (isSaleAvailable && isCostAvailable)
                    {
                        if (!sale.IsZero() && !cost.IsZero())
                        {
                            if (sale > cost) reason = $"Both Sale and Cost has value. Setting Sale as {value}.";
                            else reason = $"Both Sale and Cost has value. Setting Cost as {value * -1 }.";
                        }
                    }
                    var tradeStatement = new AdjustedTradeStatement()
                    {
                        Date = r.ReadDate(Date),
                        Name = r.ReadString(Name),
                        TransactionTax = r.ReadString(TransactionTax),
                        TransactionDetail = r.ReadString(TransactionDetail),
                        Quanity = r.ReadDouble(Quantity),
                        Value = value,
                        Reason = reason,
                    };
                    return Tuple.Create(tradeStatement, true);
                }).ToList();
                return tradeStatements.Where(x => x.Item2).Select(x => x.Item1).ToList();
            }
        }
    }
}