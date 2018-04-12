using MVC2015.Utility.Resource;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MVC2015.Web.Model.DataAnnotations
{
    /// <summary>
    /// this ValidationAttribute is just for generat the custome valid
    /// can use ErrorMessage or DisplayName
    /// </summary>
    public class RequiredExAttributeAdapter : RequiredAttributeAdapter
    {
        public RequiredExAttributeAdapter(ModelMetadata metadata, ControllerContext context, RequiredAttribute attribute)
            : base(metadata, context, attribute) { }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            string errorMessage = ResourceHelper.GetValue(this.ErrorMessage);
            return new[] { new ModelClientValidationRequiredRule(errorMessage) };
        }
    }

    public class StringLengthExAttributeAdapter : StringLengthAttributeAdapter
    {
        public StringLengthExAttributeAdapter(ModelMetadata metadata, ControllerContext context, StringLengthAttribute attribute)
            : base(metadata, context, attribute) { }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            string errorMessage = ResourceHelper.GetValue(this.ErrorMessage);
            int minLength = Attribute.MinimumLength;
            int maxLength = Attribute.MaximumLength;
            return new[] { new ModelClientValidationStringLengthRule(errorMessage, minLength, maxLength) };
        }
    }

    public class RegularExpressionIpExAttribute : RegularExpressionAttribute, IClientValidatable
    {
        public RegularExpressionIpExAttribute()
            : base(@"^((25[0-5])|(2[0-4]\d)|(1\d\d)|([1-9]\d)|\d)(\.((25[0-5])|(2[0-4]\d)|(1\d\d)|([1-9]\d)|\d)){3}$|^[a-zA-Z0-9][-a-zA-Z0-9_\\]{0,62}")
        {

        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            string errorMessage = ResourceHelper.GetValue(this.ErrorMessage);
            var rule = new ModelClientValidationRegexRule(errorMessage, Pattern);
            yield return rule;
        }
    }

    public class RegularExpressionExAttributeAdapter : RegularExpressionAttributeAdapter
    {
        public RegularExpressionExAttributeAdapter(ModelMetadata metadata, ControllerContext context, RegularExpressionAttribute attribute)
            : base(metadata, context, attribute) { }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            string errorMessage = ResourceHelper.GetValue(this.ErrorMessage);
            string pattern = Attribute.Pattern;
            return new[] { new ModelClientValidationRegexRule(errorMessage, pattern) };

        }

    }

    public class RangeExAttributeAdapter:RangeAttributeAdapter
    {
        public RangeExAttributeAdapter(ModelMetadata metadata, ControllerContext context, RangeAttribute attribute)
            : base(metadata, context, attribute) { }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            string errorMessage = ResourceHelper.GetValue(this.ErrorMessage);
            object minValue = Attribute.Minimum;
            object maxValue = Attribute.Maximum;

            return new[] { new ModelClientValidationRangeRule(errorMessage, minValue, maxValue) };
        }
    }

    public class EmailExAttribute : ValidationAttribute, IClientValidatable
    {
        public const string reg = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]+$";

        public EmailExAttribute()
        {

        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            if (value is string)
            {
                Regex regEx = new Regex(reg);
                return regEx.IsMatch(value.ToString());
            }
            return false;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ModelClientValidationRule rule = new ModelClientValidationRule
            {
                ValidationType = "email",
                //ErrorMessage = FormatErrorMessage(metadata.GetDisplayName())
                ErrorMessage = ResourceHelper.GetValue(this.ErrorMessage)
            };
            yield return rule;
        }
    }

    public class CompareExAttribute : System.ComponentModel.DataAnnotations.CompareAttribute, IClientValidatable
    {
        public string OtherProperty { get; set; }
        public CompareExAttribute(string otherProperty)
            : base(otherProperty)
        {
            if (otherProperty == null)
                throw new ArgumentNullException("otherProperty");

            OtherProperty = otherProperty;
        }

        //public override ValidationResult IsValid(object value, ValidationContext validationContext)
        //{
        //    // the the other property
        //    var property = validationContext.ObjectType.GetProperty(OtherProperty);

        //    // check it is not null
        //    if (property == null)
        //        return new ValidationResult(String.Format("Unknown property: {0}.", OtherProperty));

        //    // check types
        //    var memberName = validationContext.ObjectType.GetProperties().Where(p => p.GetCustomAttributes(false).OfType<DisplayAttribute>().Any(a => a.Name == validationContext.DisplayName)).Select(p => p.Name).FirstOrDefault();
        //    if (memberName == null)
        //    {
        //        memberName = validationContext.DisplayName;
        //    }
        //    if (validationContext.ObjectType.GetProperty(memberName).PropertyType != property.PropertyType)
        //        return new ValidationResult(ResourceHelper.GetValue(this.ErrorMessage));
        //    return new ValidationResult(ErrorMessage);
        //}

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            object other = this.OtherProperty;
            string errorMessage = ResourceHelper.GetValue(this.ErrorMessage);
            return new[] { new ModelClientValidationEqualToRule(errorMessage, other) };
        }
    }

    ///// <summary>
    ///// Specifies that the field must compare favourably with the named field, if objects to check are not of the same type
    ///// false will be return
    ///// </summary>
    //[AttributeUsageAttribute(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    //public class CompareValuesAttribute : ValidationAttribute
    //{
    //    /// <summary>
    //    /// The other property to compare to
    //    /// </summary>
    //    public string OtherProperty { get; set; }

    //    public CompareValues Criteria { get; set; }

    //    /// <summary>
    //    /// Creates the attribute
    //    /// </summary>
    //    /// <param name="otherProperty">The other property to compare to</param>
    //    public CompareValuesAttribute(string otherProperty, CompareValues criteria)
    //    {
    //        if (otherProperty == null)
    //            throw new ArgumentNullException("otherProperty");

    //        OtherProperty = otherProperty;
    //        Criteria = criteria;
    //    }

    //    /// <summary>
    //    /// Determines whether the specified value of the object is valid.  For this to be the case, the objects must be of the same type
    //    /// and satisfy the comparison criteria. Null values will return false in all cases except when both
    //    /// objects are null.  The objects will need to implement IComparable for the GreaterThan,LessThan,GreatThanOrEqualTo and LessThanOrEqualTo instances
    //    /// </summary>
    //    /// <param name="value">The value of the object to validate</param>
    //    /// <param name="validationContext">The validation context</param>
    //    /// <returns>A validation result if the object is invalid, null if the object is valid</returns>
    //    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    //    {
    //        // the the other property
    //        var property = validationContext.ObjectType.GetProperty(OtherProperty);

    //        // check it is not null
    //        if (property == null)
    //            return new ValidationResult(String.Format("Unknown property: {0}.", OtherProperty));

    //        // check types
    //        if (validationContext.ObjectType.GetProperty(validationContext.MemberName).PropertyType != property.PropertyType)
    //            return new ValidationResult(String.Format("The types of {0} and {1} must be the same.", validationContext.DisplayName, OtherProperty));

    //        // get the other value
    //        var other = property.GetValue(validationContext.ObjectInstance, null);

    //        // equals to comparison,
    //        if (Criteria == CompareValues.EqualTo)
    //        {
    //            if (Object.Equals(value, other))
    //                return null;
    //        }
    //        else if (Criteria == CompareValues.NotEqualTo)
    //        {
    //            if (!Object.Equals(value, other))
    //                return null;
    //        }
    //        else
    //        {
    //            // check that both objects are IComparables
    //            if (!(value is IComparable) || !(other is IComparable))
    //                return new ValidationResult(String.Format("{0} and {1} must both implement IComparable", validationContext.DisplayName, OtherProperty));

    //            // compare the objects
    //            var result = Comparer.Default.Compare(value, other);

    //            switch (Criteria)
    //            {
    //                case CompareValues.GreaterThan:
    //                    if (result > 0)
    //                        return null;
    //                    break;
    //                case CompareValues.LessThan:
    //                    if (result < 0)
    //                        return null;
    //                    break;
    //                case CompareValues.GreatThanOrEqualTo:
    //                    if (result >= 0)
    //                        return null;
    //                    break;
    //                case CompareValues.LessThanOrEqualTo:
    //                    if (result <= 0)
    //                        return null;
    //                    break;
    //            }
    //        }

    //        // got this far must mean the items don't meet the comparison criteria
    //        return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
    //    }

    //    /// <summary>
    //    /// Applies formatting to an error message.
    //    /// </summary>
    //    /// <param name="name">The name to include in the error message</param>
    //    /// <returns></returns>
    //    public override string FormatErrorMessage(string name)
    //    {
    //        return String.Format(CultureInfo.CurrentCulture, base.ErrorMessageString, name, OtherProperty, Criteria.Description());
    //    }

    //    /// <summary>
    //    /// retrieve the object to compare to
    //    /// </summary>
    //    /// <returns></returns>
    //    object GetOther(ValidationContext context)
    //    {
    //        return null;
    //    }
    //}

    ///// <summary>
    ///// Indicates a comparison criteria used by the CompareValues attribute
    ///// </summary>
    //public enum CompareValues
    //{
    //    [Description("=")]
    //    EqualTo,
    //    [Description("!=")]
    //    NotEqualTo,
    //    [Description(">")]
    //    GreaterThan,
    //    [Description("<")]
    //    LessThan,
    //    [Description(">=")]
    //    GreatThanOrEqualTo,
    //    [Description("<=")]
    //    LessThanOrEqualTo
    //}
}
