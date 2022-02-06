using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnluCo.NetBootcamp.Odev4.ValidationRules.Rules
{
    public class IsLowercaseValidationRule : IValidationRule
    {
        public bool IsValid(string str)
        {
            return str.Any(x => Char.IsLower(x));//Sifrede kucuk harf var mi?
        }
    }
}
