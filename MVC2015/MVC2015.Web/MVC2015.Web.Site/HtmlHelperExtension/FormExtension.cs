using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Web.WebPages;
using PH = MVC2015.Web.TypeHelper;

namespace MVC2015.Web.Site
{
    public static class FormExtensions
    {
        public static RouteValueDictionary ObjectToDictionary(object value)
        {
            RouteValueDictionary dictionary = new RouteValueDictionary();

            if (value != null)
            {
                foreach (PH.PropertyHelper helper in PH.PropertyHelper.GetProperties(value))
                {
                    dictionary.Add(helper.Name, helper.GetValue(value));
                }
            }

            return dictionary;
        }
        public static MvcForm Form(this HtmlHelper htmlHelper)
        {
            // generates <form action="{current url}" method="post">...</form>
            string formAction = htmlHelper.ViewContext.HttpContext.Request.RawUrl;
            return FormHelper(htmlHelper, formAction, FormMethod.Post, new RouteValueDictionary());
        }

        public static MvcForm Form(this HtmlHelper htmlHelper, object routeValues)
        {
            return Form(htmlHelper, null, null, ObjectToDictionary(routeValues), FormMethod.Post, new RouteValueDictionary());
        }

        public static MvcForm Form(this HtmlHelper htmlHelper, RouteValueDictionary routeValues)
        {
            return Form(htmlHelper, null, null, routeValues, FormMethod.Post, new RouteValueDictionary());
        }

        public static MvcForm Form(this HtmlHelper htmlHelper, string actionName, string controllerName)
        {
            return Form(htmlHelper, actionName, controllerName, new RouteValueDictionary(), FormMethod.Post, new RouteValueDictionary());
        }

        public static MvcForm Form(this HtmlHelper htmlHelper, string actionName, string controllerName, object routeValues)
        {
            return Form(htmlHelper, actionName, controllerName, ObjectToDictionary(routeValues), FormMethod.Post, new RouteValueDictionary());
        }

        public static MvcForm Form(this HtmlHelper htmlHelper, string actionName, string controllerName, RouteValueDictionary routeValues)
        {
            return Form(htmlHelper, actionName, controllerName, routeValues, FormMethod.Post, new RouteValueDictionary());
        }

        public static MvcForm Form(this HtmlHelper htmlHelper, string actionName, string controllerName, FormMethod method)
        {
            return Form(htmlHelper, actionName, controllerName, new RouteValueDictionary(), method, new RouteValueDictionary());
        }

        public static MvcForm Form(this HtmlHelper htmlHelper, string actionName, string controllerName, object routeValues, FormMethod method)
        {
            return Form(htmlHelper, actionName, controllerName, ObjectToDictionary(routeValues), method, new RouteValueDictionary());
        }

        public static MvcForm Form(this HtmlHelper htmlHelper, string actionName, string controllerName, RouteValueDictionary routeValues, FormMethod method)
        {
            return Form(htmlHelper, actionName, controllerName, routeValues, method, new RouteValueDictionary());
        }

        public static MvcForm Form(this HtmlHelper htmlHelper, string actionName, string controllerName, FormMethod method, object htmlAttributes)
        {
            return Form(htmlHelper, actionName, controllerName, new RouteValueDictionary(), method, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcForm Form(this HtmlHelper htmlHelper, string actionName, string controllerName, FormMethod method, IDictionary<string, object> htmlAttributes)
        {
            return Form(htmlHelper, actionName, controllerName, new RouteValueDictionary(), method, htmlAttributes);
        }

        public static MvcForm Form(this HtmlHelper htmlHelper, string actionName, string controllerName, object routeValues, FormMethod method, object htmlAttributes)
        {
            return Form(htmlHelper, actionName, controllerName, ObjectToDictionary(routeValues), method, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcForm Form(this HtmlHelper htmlHelper, string actionName, string controllerName, RouteValueDictionary routeValues, FormMethod method, IDictionary<string, object> htmlAttributes)
        {
            string formAction = UrlHelper.GenerateUrl(null /* routeName */, actionName, controllerName, routeValues, htmlHelper.RouteCollection, htmlHelper.ViewContext.RequestContext, true /* includeImplicitMvcValues */);
            return FormHelper(htmlHelper, formAction, method, htmlAttributes);
        }

        public static MvcForm RouteForm(this HtmlHelper htmlHelper, object routeValues)
        {
            return RouteForm(htmlHelper, null /* routeName */, ObjectToDictionary(routeValues), FormMethod.Post, new RouteValueDictionary());
        }

        public static MvcForm RouteForm(this HtmlHelper htmlHelper, RouteValueDictionary routeValues)
        {
            return RouteForm(htmlHelper, null /* routeName */, routeValues, FormMethod.Post, new RouteValueDictionary());
        }

        public static MvcForm RouteForm(this HtmlHelper htmlHelper, string routeName)
        {
            return RouteForm(htmlHelper, routeName, new RouteValueDictionary(), FormMethod.Post, new RouteValueDictionary());
        }

        public static MvcForm RouteForm(this HtmlHelper htmlHelper, string routeName, object routeValues)
        {
            return RouteForm(htmlHelper, routeName, ObjectToDictionary(routeValues), FormMethod.Post, new RouteValueDictionary());
        }

        public static MvcForm RouteForm(this HtmlHelper htmlHelper, string routeName, RouteValueDictionary routeValues)
        {
            return RouteForm(htmlHelper, routeName, routeValues, FormMethod.Post, new RouteValueDictionary());
        }

        public static MvcForm RouteForm(this HtmlHelper htmlHelper, string routeName, FormMethod method)
        {
            return RouteForm(htmlHelper, routeName, new RouteValueDictionary(), method, new RouteValueDictionary());
        }

        public static MvcForm RouteForm(this HtmlHelper htmlHelper, string routeName, object routeValues, FormMethod method)
        {
            return RouteForm(htmlHelper, routeName, ObjectToDictionary(routeValues), method, new RouteValueDictionary());
        }

        public static MvcForm RouteForm(this HtmlHelper htmlHelper, string routeName, RouteValueDictionary routeValues, FormMethod method)
        {
            return RouteForm(htmlHelper, routeName, routeValues, method, new RouteValueDictionary());
        }

        public static MvcForm RouteForm(this HtmlHelper htmlHelper, string routeName, FormMethod method, object htmlAttributes)
        {
            return RouteForm(htmlHelper, routeName, new RouteValueDictionary(), method, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcForm RouteForm(this HtmlHelper htmlHelper, string routeName, FormMethod method, IDictionary<string, object> htmlAttributes)
        {
            return RouteForm(htmlHelper, routeName, new RouteValueDictionary(), method, htmlAttributes);
        }

        public static MvcForm RouteForm(this HtmlHelper htmlHelper, string routeName, object routeValues, FormMethod method, object htmlAttributes)
        {
            return RouteForm(htmlHelper, routeName, ObjectToDictionary(routeValues), method, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcForm RouteForm(this HtmlHelper htmlHelper, string routeName, RouteValueDictionary routeValues, FormMethod method, IDictionary<string, object> htmlAttributes)
        {
            string formAction = UrlHelper.GenerateUrl(routeName, null, null, routeValues, htmlHelper.RouteCollection, htmlHelper.ViewContext.RequestContext, false /* includeImplicitMvcValues */);
            return FormHelper(htmlHelper, formAction, method, htmlAttributes);
        }

        public static void EndForm(this HtmlHelper htmlHelper)
        {
            EndForm(htmlHelper.ViewContext);
        }

        internal static void EndForm(ViewContext viewContext)
        {
            viewContext.Writer.Write("</form>");
            viewContext.OutputClientValidation();
            viewContext.FormContext = null;
        }
       
        private static MvcForm FormHelper(this HtmlHelper htmlHelper, string formAction, FormMethod method, IDictionary<string, object> htmlAttributes)
        {
            TagBuilder tagBuilder = new TagBuilder("form");
            tagBuilder.MergeAttributes(htmlAttributes);
            // action is implicitly generated, so htmlAttributes take precedence.
            tagBuilder.MergeAttribute("action", formAction);
            // method is an explicit parameter, so it takes precedence over the htmlAttributes.
            tagBuilder.MergeAttribute("method", HtmlHelper.GetFormMethodString(method), true);

            bool traditionalJavascriptEnabled = htmlHelper.ViewContext.ClientValidationEnabled
                                                && !htmlHelper.ViewContext.UnobtrusiveJavaScriptEnabled;

            if (traditionalJavascriptEnabled)
            {
                // forms must have an ID for client validation
                //tagBuilder.GenerateId(htmlHelper.ViewContext.FormIdGenerator());
            }

            htmlHelper.ViewContext.Writer.Write(tagBuilder.ToString(TagRenderMode.StartTag));
            //htmlHelper.ViewContext.Writer.Write(htmlHelper.AntiForgeryToken().ToHtmlString());
            htmlHelper.ViewContext.Writer.Write(htmlHelper.Hidden("__RequestVerificationToken", "value"));
            MvcForm theForm = new MvcForm(htmlHelper.ViewContext);

            if (traditionalJavascriptEnabled)
            {
                htmlHelper.ViewContext.FormContext.FormId = tagBuilder.Attributes["id"];
            }

            return theForm;
        }
    }
}