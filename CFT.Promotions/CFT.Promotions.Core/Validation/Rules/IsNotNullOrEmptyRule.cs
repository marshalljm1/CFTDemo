using System;
using System.Collections.Generic;
using System.Text;
using CFT.Promotions.Core.Validation.Interfaces;

namespace CFT.Promotions.Core.Validation.Rules
{
    public class IsNotNullOrEmptyRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }
        public bool Check(T value)
        {
            if (value == null)
                return false;

            var str = value as string;
            return !string.IsNullOrWhiteSpace(str);
        }
    }
}
