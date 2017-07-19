using System;
using System.Collections.Generic;

namespace FromRomanNumerals
{
    public static class RomanNumber
    {
        public class Digit
        {
            public int Value { get; set; }
            public int MaxSequence { get; set; }
            public bool CanSubtract { get; set; }
            public string SubtractedBy { get; set; }
        }

        private const string InvalidCharAsStarterEliminatesAtLeastOneIfClause = "$";

        private static readonly Dictionary<string, Digit> Digits = new Dictionary<string, Digit>
        { 
            { "I", new Digit { Value = 1, MaxSequence = 3, CanSubtract = true, SubtractedBy = "" } },
            { "V", new Digit { Value = 5, MaxSequence = 1, CanSubtract = false, SubtractedBy = "I" } },
            { "X", new Digit { Value = 10, MaxSequence = 3, CanSubtract = true, SubtractedBy = "I" } },
            { "L", new Digit { Value = 50, MaxSequence = 1, CanSubtract = false, SubtractedBy = "X" } },
            { "C", new Digit { Value = 100, MaxSequence = 3, CanSubtract = true, SubtractedBy = "X" } },
            { "D", new Digit { Value = 500, MaxSequence = 1, CanSubtract = false, SubtractedBy = "C" } },
            { "M", new Digit { Value = 1000, MaxSequence = 3, CanSubtract = true, SubtractedBy = "C" } },
            { InvalidCharAsStarterEliminatesAtLeastOneIfClause, new Digit { Value = int.MaxValue } },
        };

        public static bool IsDigit(string romanDigit)
        {
            return Digits.ContainsKey(romanDigit);
        }

        public static Digit GetDigit(string romanDigit)
        {
            return Digits[romanDigit];
        }

        public static bool TryToDecimal(string romanNumber, out int arabNumber)
        {
            arabNumber = 0;

            if (string.IsNullOrEmpty(romanNumber)) { return false; }

            string previousDigit = InvalidCharAsStarterEliminatesAtLeastOneIfClause;
            int countSameDigits = 0;
            bool justSubtracted = false;

            foreach (char romanDigit in romanNumber) {
                var nextDigit = new String(romanDigit, 1);
                if (!Digits.ContainsKey(nextDigit)) { return false; }

                if (previousDigit == nextDigit) {
                    countSameDigits++;
                    if (countSameDigits > Digits[previousDigit].MaxSequence) { return false; }
                } else {
                    var subtractBecauseNextIsLarger = Digits[nextDigit].Value > Digits[previousDigit].Value;
                    if (subtractBecauseNextIsLarger) {
                        if (justSubtracted) { return false; }
                        if (!Digits[previousDigit].CanSubtract) { return false; }
                        if (previousDigit != Digits[nextDigit].SubtractedBy) { return false; }
                        if (countSameDigits > 1) { return false; }
                    }
                    arabNumber += (subtractBecauseNextIsLarger ? -1 : 1) * Digits[previousDigit].Value * countSameDigits;
                    previousDigit = nextDigit;
                    countSameDigits = 1;
                    justSubtracted = subtractBecauseNextIsLarger;
                }
            }

            // Dont forget to collect the last "previousDigit", always add
            arabNumber += Digits[previousDigit].Value * countSameDigits;

            return true;
        }

        public static int ToDecimal(string romanNumber)
        {
            int arabNumber;
            
            if (!TryToDecimal(romanNumber, out arabNumber)) {
                throw new Exception("RomanNumbers.ToDecimal('" + romanNumber + "') failed");
            }

            return arabNumber;
        }
    }
}
