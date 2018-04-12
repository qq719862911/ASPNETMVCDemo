using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.ComponentModel;
using MVC2015.Web.Site.Common;
using MVC2015.Web.Model.Common;
using MVC2015.Web.Model.DataAnnotations;

namespace MVC2015.Web.Site
{
    public static partial class HtmlHelperExtend
    {
        public static MvcHtmlString RadioListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression,
            int cols, object warpHtmlAttribute, object itemHtmlAttribute, int width = 0, int indent = 0)
        {
            string tableClassStr = "";
            string tdClassStr = "";
            string result = "";

            string radioPropertyName;
            string displayPropertyName;

            MemberExpression nameExpr = (MemberExpression)expression.Body;
            radioPropertyName = nameExpr.Member.Name;
            // 获取特性
            PropertyInfo property = typeof(TModel).GetProperty(radioPropertyName);
            KeyValueAttribute attribute = (KeyValueAttribute)property.GetCustomAttributes(typeof(KeyValueAttribute), false)[0];

            if (attribute == null)
            {
                throw new Exception();
            }
            displayPropertyName = attribute.DisplayProperty;
            //获取显示列表 和选择值
            IEnumerable radioList = (IEnumerable)htmlHelper.ViewData.Eval(displayPropertyName);
            var checkValue = htmlHelper.ViewData.Eval(radioPropertyName);
            // 转换objectHtmlAttrbute
            IDictionary<string, object> wrapKeyValue = HtmlHelper.AnonymousObjectToHtmlAttributes(warpHtmlAttribute);
            IDictionary<string, object> itemKeyValue = HtmlHelper.AnonymousObjectToHtmlAttributes(itemHtmlAttribute);

            // 生成Html
            if (wrapKeyValue.Count > 0) // wrapClassStr
            {
                foreach (KeyValuePair<string, object> keyValue in wrapKeyValue)
                {
                    tableClassStr += keyValue.Key + "=" + keyValue.Value.ToString();
                }
            }
            //if (itemKeyValue.Count > 0)
            //{
            //    foreach (KeyValuePair<string, object> keyValue in itemKeyValue)
            //    {
            //        tdClassStr += keyValue.Key + "='" + keyValue.Value.ToString() + "' ";
            //    }
            //}

            int index = 0;
            result += "<table " + tableClassStr + ">";
            foreach (var radio in radioList)
            {
                if (index % cols == 0)
                {
                    result += "<tr>";
                }
                
                KeyValueModel radioModel = (KeyValueModel)radio;
                bool isCheck = checkValue == null ? false : checkValue.ToString() == radioModel.Value;
                if (width > 0)
                {
                    result += "<td width=\"" + width + "px\" style=\"margin-right:" + indent + "px; display:inline-block;\">";
                }
                else
                {
                    result += "<td style=\"margin-right:" + indent + "px; display:inline-block;\" " + ">"; //"<td " + tdClassStr + ">";
                }
                result += (htmlHelper.RadioButton(radioPropertyName, radioModel.Value, isCheck, itemHtmlAttribute)).ToHtmlString();
                result += radioModel.Text; //ResourceHelper.GetResourceObject(typeof(CommonResource), radioModel.ResourceKey)
                result += "</td>";

                if (index % cols == cols - 1)
                {
                    result += "</tr>";
                }
                index += 1;
            }
            result += "</table>";

            return new MvcHtmlString(result);
        }

        public static MvcHtmlString RadioListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression,
            int cols)
        {
            return RadioListFor(htmlHelper, expression, cols, null, null);
        }
        public static MvcHtmlString RadioListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, int cols, int width)
        {
            return RadioListFor(htmlHelper, expression, cols, null, null, width);
        }
        public static MvcHtmlString RadioListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, int cols, int width, int indent)
        {
            return RadioListFor(htmlHelper, expression, cols, null, null, width, indent);
        }
        public static MvcHtmlString RadioListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression,
            DisplayDirection direction, int cols)
        {
            if (direction == DisplayDirection.Vertical)
            {
                return RadioListFor(htmlHelper, expression, 1, null, null);
            }
            else
            {
                return RadioListFor(htmlHelper, expression, cols, null, null);
            }
        }

        public static MvcHtmlString RadioListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return RadioListFor(htmlHelper, expression, 1, null, null);
        }

        public static MvcHtmlString RadioListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression,
    DisplayDirection direction)
        {
            return RadioListFor(htmlHelper, expression, direction, 15);
        }
    }
}