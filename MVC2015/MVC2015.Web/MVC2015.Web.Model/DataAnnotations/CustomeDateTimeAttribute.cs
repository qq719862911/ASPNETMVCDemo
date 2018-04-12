using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MVC2015.Web.Model.DataAnnotations
{
    /// <summary>
    /// no finish
    /// this ValidationAttribute is just for generat the custome valid
    /// when the value post back to service, will use "DateTimeBinder", "MyDateTimeModelBinder" class to convert the value
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class CustomeDateTimeAttribute : ValidationAttribute, IClientValidatable
    {
        //private string _dateFormate;
        public string DateFormatProperty { get; set; }
        public string DisplayName { get; set; }
        public CustomeDateTimeAttribute()
        {
            //_dateFormatProperty = dateFormatProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //var property = validationContext.ObjectType.GetProperty(DateFormatProperty);
            //if (property == null)
            //{
            //    return new ValidationResult(string.Format(CultureInfo.CurrentCulture, "{0} 不存在", DateFormatProperty));
            //}
            //else
            //{
            //    string dateFromat = property.GetValue(validationContext.ObjectInstance).ToString();
            //    _dateFormate = dateFromat;
            //    // no finish
            //    //return base.IsValid(value, validationContext);
            //}
            return null;
        }

        //public override bool IsValid(object value)
        //{
        //    return base.IsValid(value);
        //}


        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                //ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),

                // ValidationType - 指定一个 key（字符串），该 key 用于关联服务端验证逻辑与客户端验证逻辑。注：这个 key 必须都是由小写字母组成
                ValidationType = "customedate"
            };

            var parentType = metadata.ContainerType;
            var parentMetaData = ModelMetadataProviders.Current.GetMetadataForProperties(context.Controller.ViewData.Model, parentType);
            var _dateFormate = (string)parentMetaData.FirstOrDefault(p => p.PropertyName == DateFormatProperty).Model;

            // 向客户端验证代码传递参数
            //var property =  req ObjectType.GetProperty(DateFormatProperty);
            rule.ValidationParameters.Add("dateformate", _dateFormate);

            var property = parentType.GetProperty(metadata.PropertyName);

            //var msgType = customeDateAtt.ErrorMessageResourceType;
            //var msgKey = customeDateAtt.ErrorMessageResourceName;
            //var msgDisplayName = customeDateAtt.DisplayName;


            rule.ErrorMessage = "Error";

            yield return rule;
        }
    }
}
