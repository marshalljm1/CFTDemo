using System;
using System.Collections.Generic;
using System.Text;

namespace CFT.Promotions.Core.Validation.Interfaces
{
    public interface IValidationRule<T>
    {
        string ValidationMessage { get; set; }
        bool Check(T value);
    }
}
