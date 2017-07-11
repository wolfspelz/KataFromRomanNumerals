using System;
using System.Collections.Generic;
using GalacticMerchant.Statements;

namespace GalacticMerchant
{
    public class Calculator
    {
        private readonly Dictionary<string, string> romanDigitAliases = new Dictionary<string, string>();
        private readonly Dictionary<string, double> resourceValuesInCredits = new Dictionary<string, double>();

        public readonly ComponentLogger Log = new ComponentLogger();

        #region External interface

        public IStatement Parse(string text)
        {
            var candidateStatements = new List<IStatement>
            {
                new RomanAliasDefinition(this),
                new MarketValueDefinition(this),
                new RomanNumberQuery(this),
                new MarketValueQuery(this),
                new AnyOtherStatement(this),
            };

            foreach (var candidate in candidateStatements) {
                if (candidate.Parse(text)) {
                    Log.Info(candidate.GetType().Name + ": " + candidate + " (" + text + ")");
                    return candidate;
                }
                Log.Verbose(candidate.GetType().Name + ": " + candidate.GetReason() + " (" + text + ")");
            }

            throw new Exception("Should never reach this, because " + typeof(AnyOtherStatement).Name + " catches all statements");
        }

        public void LearnRomanDigitAlias(string alias, string digit)
        {
            romanDigitAliases.Add(alias, digit);
        }

        public void LearnResourceValue(string resource, double value)
        {
            resourceValuesInCredits.Add(resource, value);
        }

        #endregion

        #region Statement interface

        public bool IsRomanDigit(string digit)
        {
            return RomanNumber.IsDigit(digit);
        }

        public bool IsRomanDigitAlias(string alias)
        {
            return romanDigitAliases.ContainsKey(alias);
        }

        public string GetRomanDigitByAlias(string alias)
        {
            return romanDigitAliases[alias];
        }

        public bool IsResource(string resourceName)
        {
            return resourceValuesInCredits.ContainsKey(resourceName);
        }

        public double GetResourceValueInCredits(string resourceName)
        {
            return resourceValuesInCredits[resourceName];
        }

        #endregion

    }
}
