using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using MVC2015.Utility.Resource;


namespace MVC2015.Web.Model.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class DateTimeNotLessThan : ValidationAttribute, IClientValidatable
    {
        private const string DefaultErrorMessage = "{0} 不得少于 {1}";

        public string OtherProperty { get; private set; }
        private string OtherPropertyName { get; set; }

        public DateTimeNotLessThan(string otherProperty, string otherPropertyName)
            : base(DefaultErrorMessage)
        {
            if (string.IsNullOrEmpty(otherProperty))
            {
                throw new ArgumentNullException("otherProperty");
            }

            OtherProperty = otherProperty;
            OtherPropertyName = ResourceHelper.GetValue(otherPropertyName);
        }

        public override string FormatErrorMessage(string name)
        {
            var PropertyName = ResourceHelper.GetValue(name);
            var NotLessThan = ResourceHelper.GetValue("Message_Common_NotLessThan");
            return string.Format(NotLessThan, PropertyName, OtherPropertyName);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var otherProperty = validationContext.ObjectInstance.GetType().GetProperty(OtherProperty);
                var otherPropertyValue = otherProperty.GetValue(validationContext.ObjectInstance, null);

                DateTime dtThis = Convert.ToDateTime(value);
                DateTime dtOther = Convert.ToDateTime(otherPropertyValue);

                if (dtThis < dtOther)
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
            }
            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(
                                                      ModelMetadata metadata,
                                                      ControllerContext context)
        {
            var clientValidationRule = new ModelClientValidationRule()
            {
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = "notlessthan"//这里是核心点
            };

            clientValidationRule.ValidationParameters.Add("otherproperty", OtherProperty);

            //return new[] { clientValidationRule };
            yield return clientValidationRule;
        }
    }
}
