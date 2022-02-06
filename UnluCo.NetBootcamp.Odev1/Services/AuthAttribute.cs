using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using UnluCo.NetBootcamp.Odev4.ValidationRules;
using UnluCo.NetBootcamp.Odev4.ValidationRules.Rules;

namespace UnluCo.NetBootcamp.Odev4.Services
{
    public class AuthAttribute : ValidationAttribute
    {
        public int MinLength { get; set; }

        public AuthAttribute()
        {
            MinLength = 8;//MinLength deger atanmazsa default olarak 8 olur.
        }
        public IValidationRule[] GetRules()
        {
            IValidationRule[] rules = {
                new IsDigitValidationRule(),
                new IsLengthValidationRule(MinLength),
                new IsLowercaseValidationRule(),
                new IsUppercaseValidationRule()
             };//Yapilacak kontrollerin listesi.
            return rules;
        }
        public override bool IsValid(object value)
        {
            string password = (string)value;
            bool result = false;
            foreach (var rule in GetRules())
            {
                result = rule.IsValid(password);//Verilen kontrol türleri tek tek denenir.
                if (result is false) { 
                    ErrorMessage = string.Format("Şifre en az {0} karakter uzunluğunda olmalı, büyük/küçük harf ve rakam bulunmalı!",MinLength);
                    //context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    return false; 
                }//Kontrolden gecemezse false doner.
            }
            return true;
        }
    }
}
