using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Forum.Views.Helpers
{
    public static class HtmlExtentions
    {
        /// <summary>
        /// Рисует ссылку если у пользователя есть роль
        /// </summary>
        /// <param name="html">htmlHelper</param>
        /// <param name="requiredRoleName">название роли</param>
        /// <param name="text">текст ссылки</param>
        /// <param name="actionName">название ActionResult</param>
        /// <param name="controllerName">название Controller</param>
        /// <param name="routedValue">параметр, передаваемый в метод Controller'а</param>
        /// <param name="controllerName">html атрибуты</param>
        /// <returns>secure ActionLink</returns>
        public static MvcHtmlString ActionLinkWithRole(this HtmlHelper html,
                   string requiredRoleName, string text,
                   string actionName, string controllerName,
                   object routedValue = null, object htmlAttributes = null)
        {
            HttpContext context = HttpContext.Current;
            MvcHandler handler = context.Handler as MvcHandler;
            if (handler.RequestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                if (handler.RequestContext.HttpContext.User.IsInRole(requiredRoleName))
                {
                    return html.ActionLink(text, actionName, controllerName, routedValue, htmlAttributes);
                }
            }
            return MvcHtmlString.Empty;
        }


        public static MvcHtmlString FormWithRole(this HtmlHelper htmlHelper, string requiredRoleName,
            string actionName, string controllerName, string sumbitCaption,
            Object routeValues = null, Object htmlAttributes = null, FormMethod method = FormMethod.Post)
        {
            HttpContext context = HttpContext.Current;
            MvcHandler handler = context.Handler as MvcHandler;
            if (handler.RequestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                if (handler.RequestContext.HttpContext.User.IsInRole(requiredRoleName))
                {
                    var tag = new TagBuilder("form");
                    tag.MergeAttribute("method", method.ToString());
                    var helper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
                    var url = helper.Action(actionName, controllerName, routeValues);
                    tag.MergeAttribute("action", url);

                    var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                    foreach (var attribute in attributes)
                    {
                        tag.MergeAttribute(attribute.Key.ToString(), attribute.Value.ToString());
                    }

                    var button = new TagBuilder("button");
                    button.MergeAttribute("type", "sumbit");
                    button.MergeAttribute("class", "btn btn-default");
                    button.InnerHtml = sumbitCaption;

                    tag.InnerHtml += button.ToString();

                    return MvcHtmlString.Create(tag.ToString());
                }
            }

            return MvcHtmlString.Empty;
        }

        public static MvcHtmlString FormForAutorOrAdmin(this HtmlHelper htmlHelper, string autorName,
            string actionName, string controllerName, string sumbitCaption,
            Object routeValues = null, Object htmlAttributes = null, FormMethod method = FormMethod.Post)
        {
            if (autorName == HttpContext.Current.User.Identity.Name || HttpContext.Current.User.IsInRole("admin"))
            {
                return FormWithRole(htmlHelper, "user", actionName, controllerName, sumbitCaption, routeValues, htmlAttributes, method);
            }
            else
            {
                return MvcHtmlString.Empty;
            }
        }
    }
}