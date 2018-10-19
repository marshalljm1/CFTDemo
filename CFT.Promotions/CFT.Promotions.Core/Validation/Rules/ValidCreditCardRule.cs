using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using CFT.Promotions.Core.Validation.Interfaces;

namespace CFT.Promotions.Core.Validation.Rules
{
    public class ValidCreditCardRule<T> : IValidationRule<T>
    {
        private readonly Regex _visaRegex = new Regex(@"^4[0-9]{6,}$");
        private readonly Regex _masterRegex = new Regex(@"^5[1-5][0-9]{5,}|222[1-9][0-9]{3,}|22[3-9][0-9]{4,}|2[3-6][0-9]{5,}|27[01][0-9]{4,}|2720[0-9]{3,}$");
        private readonly Regex _dinnersRegex = new Regex(@"^3(?:0[0-5]|[68][0-9])[0-9]{4,}$");
        private readonly Regex _discoverRegex = new Regex(@"^6(?:011|5[0-9]{2})[0-9]{3,}$");
        private readonly Regex _jcbRegex = new Regex(@"^(?:2131|1800|35[0-9]{3})[0-9]{3,}$");
        private readonly Regex _amexRegex = new Regex(@"^3[47][0-9]{5,}$");

        public string ValidationMessage { get; set; }
        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var str = value as string;
            var visaMatch = _visaRegex.Match(str);
            var masterMatch = _masterRegex.Match(str);
            var dinnersMatch = _dinnersRegex.Match(str);
            var discoverMatch = _discoverRegex.Match(str);
            var jcbMatch = _jcbRegex.Match(str);
            var amexMatch = _amexRegex.Match(str);

            return visaMatch.Success || masterMatch.Success ||
                   dinnersMatch.Success || discoverMatch.Success ||
                   jcbMatch.Success || amexMatch.Success;
        }
    }
}
