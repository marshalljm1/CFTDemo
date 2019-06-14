using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using CFT.Promotions.Core.Validation.Interfaces;

namespace CFT.Promotions.Core.Validation.Rules
{
    public class ValidExpiryDateRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }
        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            try
            {
                var exp = DateTime.ParseExact(value as string, "MM/yy", CultureInfo.CurrentCulture);
                return exp > DateTime.Now;
            }
            catch
            {
                return false;
            }

            
        }
    }
}
