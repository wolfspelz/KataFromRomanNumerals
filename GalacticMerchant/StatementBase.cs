using System;
using System.Collections.Generic;
using System.Linq;

namespace GalacticMerchant
{
    public class StatementBase
    {
        protected Calculator Calculator { get; set; }
        protected ComponentLogger Log { get { return Calculator.Log; } }

        #region Abort reason

        private string ReasonMessage = "";

        protected StatementBase(Calculator calculator)
        {
            Calculator = calculator;
        }

        protected bool Reason(string message)
        {
            ReasonMessage = message;
            return false;
        }

        public string GetReason()
        {
            return ReasonMessage;
        }

        #endregion

        #region Parser helpers

        protected string StripLeading(string text, string prefixString)
        {
            if (text.StartsWith(prefixString)) {
                text = text.Substring(prefixString.Length);
            }
            return text;
        }

        protected string StripTrailing(string text, string suffixString)
        {
            if (text.EndsWith(suffixString)) {
                text = text.Substring(0, text.Length - suffixString.Length);
            }
            return text;
        }

        protected List<string> StripTrailing(List<string> tokens, string suffixToken)
        {
            if (tokens.Last() == suffixToken) {
                tokens.RemoveAt(tokens.Count - 1);
            }
            return tokens;
        }

        protected List<string> Tokenize(string text)
        {
            return text.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        #endregion

    }
}
