using System;
using System.Collections.Generic;
using System.Linq;
using Nachiappan.TradingAssistantViewModel.Model.Excel;
using Nachiappan.TradingAssistantViewModel.Model.Statements;

namespace Nachiappan.TradingAssistantViewModel.Model.ExcelGateway
{
    public class JournalGateway
    {
        private readonly string _inputFile;

        private List<string> _headings = new List<string>()
        {
             "S.No.", "Date",  "Account", "Tag",  "Description", "Credit" , "Debit" 
        };

        const int SerialNumber = 0;
        const int Date = 1;
        const int LedgerName = 2;
        const int Tag = 3;
        const int Description = 4;
        const int Credit = 5;
        const int Debit = 6;

        public JournalGateway(string inputFile)
        {
            _inputFile = inputFile;
        }

        public void WriteJournal(IList<JournalStatement> journalStatements)
        {
            using (ExcelSheetWriter writer = new ExcelSheetWriter(_inputFile, "Journal"))
            {
                int index = 0;

                writer.Write(index++, _headings.ToArray());
                writer.SetColumnsWidth(6, 12, 35, 30, 45, 12, 12);
                writer.ApplyHeadingFormat(_headings.Count);
                writer.WriteList(index, journalStatements.OrderBy(x => x.Date).ToList(),
                    (j, rowIndex) => new object[]
                    {
                        rowIndex - 1,
                        j.Date,
                        j.Account,
                        j.Tag,
                        j.Description,
                    });

            }
        }


        public List<JournalStatement> GetJournalStatements(ILogger logger, string sheetName)
        {

            using (ExcelReader reader = new ExcelReader(_inputFile, sheetName, logger))
            {

                SheetHeadingVerifier.VerifyHeadingNames(logger, reader, _headings);

                var journalStatements = reader.ReadAllLines(1, (r) =>
                {
                    if(!r.IsValueAvailable(SerialNumber)) return Tuple.Create(new JournalStatement(),false);
                    var isCreditAvailable = r.IsValueAvailable(Credit);
                    var credit = isCreditAvailable ? r.ReadDouble(Credit) : 0;
                    var isDebitAvailable = r.IsValueAvailable(Debit);
                    var debit = isDebitAvailable ? r.ReadDouble(Debit) : 0;
                    
                    var journalStatement =  new JournalStatement()
                    {
                        Date = r.ReadDate(Date),
                        Account = r.ReadString(LedgerName),
                        Tag = r.ReadString(Tag),
                        Description = r.ReadString(Description),
                        Value = credit - debit,
                    };
                    return Tuple.Create(journalStatement,true);
                }).ToList();
                

                return journalStatements.Where(x=> x.Item2).Select(x => x.Item1).ToList();
            }
        }
    }
}