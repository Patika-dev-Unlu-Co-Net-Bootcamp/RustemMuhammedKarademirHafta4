using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnluCo.NetBootcamp.Odev4.ValidationRules.Rules
{
    public class IsUppercaseValidationRule : IValidationRule
    {
        public bool IsValid(string str)
        {
            return str.Any(x => Char.IsUpper(x));//Sifrede buyuk harf var mi?
        }
    }
}
