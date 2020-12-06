using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SFTEST.CustomerAttribute
{
    public class CustomerDateAttribute : ValidationAttribute, IClientValidatable
    {
        private DateTime _MinDate;

        public CustomerDateAttribute()
        {
            _MinDate = DateTime.Today;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime _EndDat = DateTime.Parse(value.ToString(), CultureInfo.InvariantCulture);
            DateTime _CurDate = DateTime.Today;

            int cmp = _EndDat.CompareTo(_CurDate);
            if (cmp >= 0)
            {
                // deadline date should  not be less than current date
                return ValidationResult.Success;
            }
            else 
            {
                // raise error message
                return new ValidationResult(ErrorMessage);
            }
           
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = "restrictbackdates",
            };
            rule.ValidationParameters.Add("mindate", _MinDate);
            yield return rule;
        }
    }
}