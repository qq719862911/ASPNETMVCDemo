using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using MVC2015.Web.Model.Common;
using MVC2015.Web.Model.DataAnnotations;
using MVC2015.Web.Site.Common;
using MVC2015.Utility.Resource;

namespace MVC2015.Web.Site
{
    public static partial class HtmlHelperExtend
    {
        public static MvcHtmlString CheckListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression,
            bool vaild, DisplayDirection direction, int colOrRows, object htmlAttribute, object itemAttribute, object checkAttribute = null)
        {
            string resultStr = "";

            string checkPropertyName;
            string displayPropertyName;

            MemberExpression nameExpr = (MemberExpression)expression.Body;
            checkPropertyName = nameExpr.Member.Name;

            PropertyInfo property = typeof(TModel).GetProperty(checkPropertyName);
            KeyValueAttribute attribute = (KeyValueAttribute)property.GetCustomAttributes(typeof(KeyValueAttribute), false)[0];


            if (attribute == null)
            {
                throw new Exception();
            }
            displayPropertyName = attribute.DisplayProperty;

            string needVaild = "";
            if (vaild)
            {
                needVaild = "_needVaild";
            }

            string validAttribute = "";
            //RequiredAttribute requiredAttribute = (RequiredAttribute)property.GetCustomAttributes(typeof(RequiredAttribute), false)[0];
            RequiredAttribute requiredAttribute = new RequiredAttribute();
            if (property.GetCustomAttributes(typeof(RequiredAttribute), false).Count() > 0)
                requiredAttribute = (RequiredAttribute)property.GetCustomAttributes(typeof(RequiredAttribute), false)[0];
            if (requiredAttribute != null)
            {
                validAttribute += " data-val='true' ";
                if (String.IsNullOrEmpty(requiredAttribute.ErrorMessage))
                {
                    string template = "The {0} field is required.";
                    validAttribute += " data-val-required='" + string.Format(template, displayPropertyName) + "' ";
                }
                else
                {
                    validAttribute += " data-val-required='" + ResourceHelper.GetValue(requiredAttribute.ErrorMessage) + "' ";
                }
            }

            IEnumerable checkList = (IEnumerable)htmlHelper.ViewData.Eval(displayPropertyName);
            var isCheckedList = htmlHelper.ViewData.Eval(checkPropertyName) as IEnumerable;

            IDictionary<string, object> htmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttribute);
            IDictionary<string, object> itemAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(itemAttribute);
            IDictionary<string, object> checkAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(checkAttribute);

            string htmlAttributeStr = "";
            foreach (var item in htmlAttributes)
            {
                htmlAttributeStr += " " + item.Key + "='" + item.Value + "' ";
            }
            string itemAttributeStr = "";
            foreach (var item in itemAttributes)
            {
                itemAttributeStr += " " + item.Key + "='" + item.Value + "' ";
            }
            string checkAttributeStr = "";
            foreach (var item in checkAttributes)
            {
                checkAttributeStr += " " + item.Key + "='" + item.Value + "' ";
            }


            string directionAppend = "";
            if (direction == DisplayDirection.Vertical)
                directionAppend = "<br/>";

            int index = 1;
            resultStr += "<div " + htmlAttributeStr + ">";
            foreach (KeyValueModel check in checkList)
            {
                if (check.Disable == "disabled" && check.Disable != null)
                {
                    index--;
                    resultStr += "<label style=\"display:none\">";
                }
                else
                {
                    resultStr += "<label " + itemAttributeStr + ">";
                }
                string checkedStr = "";
                bool isCheck = false;
                if (isCheckedList != null)
                {
                    foreach (var checkvalue in isCheckedList)
                    {
                        isCheck = check.Value == checkvalue.ToString();
                        if (isCheck)
                        {
                            checkedStr = " checked='checked' ";
                            break;
                        }
                    }
                }
                KeyValueModel checkModel = (KeyValueModel)check;

                if (check.Disable == "disabled" && check.Disable != null)
                {
                    resultStr += "<input" + validAttribute + " class='" + check.Disable + "'style=\"display:none\" id='" + checkPropertyName + needVaild + "' name='" + checkPropertyName + "' type=\"checkbox\" value='" + check.Value + "'" + checkedStr + checkAttributeStr + " ></input>" + " ";
                    resultStr += checkModel.Text;
                }
                else
                {
                    resultStr += "<input" + validAttribute + " name='" + checkPropertyName + "' id='" + checkPropertyName + needVaild + "' type=\"checkbox\" value='" + check.Value + "'" + checkedStr + checkAttributeStr + " ></input>" + " ";
                    resultStr += checkModel.Text;
                }

                resultStr += "</label>";
                if (index % colOrRows == 0)
                {
                    resultStr += directionAppend;
                }
                index = index + 1;
            }
            if (resultStr == "<div >")
            {
                resultStr += "<label " + itemAttributeStr + " style=\"display:none\"><input" + validAttribute + " name='" + checkPropertyName + "' id='" + checkPropertyName + needVaild + "' type=\"checkbox\" value='1' "+ checkAttributeStr + " style=\"visibility:hidden\"></input></label>";
            }
            resultStr += "</div>";

            MvcHtmlString result = new MvcHtmlString(resultStr);
            return result;
        }

        public static MvcHtmlString CheckListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, DisplayDirection direction, object htmlAttribute, object itemAttribute, object checkAttribute = null)
        {
            return CheckListFor(htmlHelper, expression, false, direction, 2, htmlAttribute, itemAttribute, checkAttribute);
        }

        public static MvcHtmlString CheckListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, bool vaild, DisplayDirection direction, object htmlAttribute)
        {
            return CheckListFor(htmlHelper, expression, vaild, direction, 2, null, new { style = "display:inline-block;font-size: inherit; font-weight: inherit;margin-right:10px;" });
        }

        public static MvcHtmlString CheckListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, DisplayDirection direction, object htmlAttribute)
        {
            return CheckListFor(htmlHelper, expression, false, direction, 2, null, new { style = "display:inline-block;font-size: inherit; font-weight: inherit;margin-right:10px;" });
        }
    }
}