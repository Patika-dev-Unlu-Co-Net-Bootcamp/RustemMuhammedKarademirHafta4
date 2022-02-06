using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnluCo.NetBootcamp.Odev4.ValidationRules.Rules
{
    public class IsLengthValidationRule : IValidationRule
    {
        public int MinLength { get; set; }
        public IsLengthValidationRule(int minLength)
        {
            MinLength = minLength;
        }
        public bool IsValid(string str)
        {
            return str.Length >= MinLength;//Sifre uzunlugu paratemre olarak gelen MinLength den fazla mi?
        }
    }
}
