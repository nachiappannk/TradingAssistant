using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Nachiappan.TradingAssistantViewModel.Model.Statements;

namespace Nachiappan.TradingAssistantViewModel.Model
{
    public class RecordedEventCleaner
    {
        private const double DoubleComparisonThreshold = 0.001;

        public List<CleanedTradeEvent> CleanTradeEvents(List<RecordedTradeEvent> recordedTradeEvents)
        {
            return CleanTradeEventsInternal(recordedTradeEvents).ToList();
        }

        private IEnumerable<CleanedTradeEvent> CleanTradeEventsInternal(List<RecordedTradeEvent> recordedTradeEvents)
        {
            int previousSerialNumber = 0;
            DateTime previousDate = new DateTime(0001, 01, 01);
            foreach (var r in recordedTradeEvents)
            {
                if (r.SerialNumber <= previousSerialNumber)
                    yield return CreateInvalidEvent(r, "The serial Number is not assending order");
                if (r.Date < previousDate)
                    yield return CreateInvalidEvent(r, "The date is not correct is assending order");
                previousSerialNumber = r.SerialNumber;
                previousDate = r.Date;

                var cleanedTradeEvent = new CleanedTradeEvent(r);
                cleanedTradeEvent = Adjust(cleanedTradeEvent);
                yield return cleanedTradeEvent;
            }
        }

        private CleanedTradeEvent Adjust(CleanedTradeEvent cleanedTradeEvent)
        {
            if (cleanedTradeEvent.Name.ToLower().StartsWith("##command"))
            {
                return cleanedTradeEvent;
            }
            else
            {
                return AdjustTradeEvent(cleanedTradeEvent);
            }
        }

        private CleanedTradeEvent AdjustTradeEvent(CleanedTradeEvent cleanedTradeEvent)
        {
            AdjustName(cleanedTradeEvent);
            AdjustQuantity(cleanedTradeEvent);
            AdjustValue(cleanedTradeEvent);
            return cleanedTradeEvent;
        }

        private void AdjustName(CleanedTradeEvent st)
        {
            var name = st.Name;
            st.Name = GetCorrectedName(name);
            if (st.Name != name)
                st.AddReason($"The input '{name}' is adjusted as $'{st.Name}'.");
        }

        private void AdjustQuantity(CleanedTradeEvent st)
        {
            if (st.Quanity.HasValue && st.Quanity.Value >= DoubleComparisonThreshold) return;
            st.Reason = "The quanity is invalid";
            st.IsValid = true;
        }

        private void AdjustValue(CleanedTradeEvent st)
        {
            AdjustValueWhenSaleAndCostPresent(st);
            //TODO When both the values are not present is cosidered okay for now. This should be disallowed and we should handle bonus as a command
        }

        private static void AdjustValueWhenSaleAndCostPresent(CleanedTradeEvent st)
        {
            if (!st.CostValue.HasValue) return;
            if (!st.SaleValue.HasValue) return;
            if (st.SaleValue.Value < DoubleComparisonThreshold) return;
            if (st.CostValue.Value < DoubleComparisonThreshold) return;
            st.AddReason($"Adjusting Cost{st.CostValue.Value}/Sale{st.SaleValue.Value} as both have value");
            st.IsAdjusted = true;
            if (st.CostValue.Value > st.SaleValue.Value)
            {
                st.CostValue = st.CostValue - st.SaleValue;
                st.SaleValue = null;
            }
            else
            {
                st.SaleValue = st.SaleValue - st.CostValue;
                st.CostValue = null;
            }
        }


        private static string GetCorrectedName(string input)
        {
            var output = input;
            output = output.Trim();
            output = Regex.Replace(output, @"\s+", " ");
            output = Regex.Replace(output, @"[^0-9a-zA-Z&\s]+", "");
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

        CleanedTradeEvent CreateInvalidEvent(RecordedTradeEvent tradeEvent, string reason)
        {
            var cleanedTradeEvent = new CleanedTradeEvent(tradeEvent)
            {
                IsValid = false,
                Reason = reason,
            };
            return cleanedTradeEvent;
        }
    }
}