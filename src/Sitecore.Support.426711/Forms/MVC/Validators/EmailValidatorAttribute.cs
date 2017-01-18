using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Forms.Mvc.Models;
using Sitecore.Forms.Mvc.Validators;


namespace Sitecore.Support.Forms.Mvc.Validators
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class EmailValidatorAttribute : DynamicValidationBase
    {
        private static readonly Regex Regex;

        static EmailValidatorAttribute()
        {
            //try to get value from validation expresiion from /sitecore/system/Modules/Web Forms for Marketers/Settings/Validation/E-mail item
            Item item = Context.Database.GetItem("{5D10AF75-3305-4C39-908E-B25E8CB4ABDC}");
            if (item != null)
            {
                Field field = item.Fields["{C64697F6-73FC-470B-AA05-11770FA6A813}"];
                if (field != null)
                {
                    string value = field.Value;
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        EmailValidatorAttribute.Regex = new Regex(value, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);
                        return;
                    }
                }
            }
            EmailValidatorAttribute.Regex = new Regex("^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,4}$", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);
        }
        #region unmodified
        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            FieldModel model = base.GetModel(metadata);
            yield return new ModelClientValidationRegexRule(this.FormatErrorMessage(model, new object[0]), EmailValidatorAttribute.Regex.ToString());
            yield break;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (this.MatchesMask(value))
            {
                return ValidationResult.Success;
            }
            FieldModel model = base.GetModel(validationContext);
            return new ValidationResult(this.FormatErrorMessage(model, new object[0]));
        }

        private bool MatchesMask(object value)
        {
            return value == null || string.IsNullOrEmpty((string)value) || EmailValidatorAttribute.Regex.IsMatch((string)value);
        }
        #endregion 
    }
}