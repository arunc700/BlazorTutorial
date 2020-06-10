using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAppDemo.CustomeValidator
{
    public class EmailDomainValidator : ValidationAttribute
    {
        public string AllowDomain { get; set; }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string[] str = value.ToString().Split('@');
                if (str.Length > 1 && str[1].ToUpper() == AllowDomain.ToUpper())
                {
                    return null;
                }

                return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
            }
            return null;
            //return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
            //return base.IsValid(value, validationContext);
        }
    }
}
