using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace FailTracker.Helpers
{
    public static class AngularTemplateHelper
    {
        private static readonly Dictionary<Type, string> TemplateMap
            = new Dictionary<Type, string>
            {
                {typeof(byte), "Number"},
                {typeof(sbyte), "Number"},
                {typeof(int), "Number"},
                {typeof(uint), "Number"},
                {typeof(long), "Number"},
                {typeof(ulong), "Number"},
                {typeof(bool), "Boolean"},
                {typeof(decimal), "Decimal"},
            };

        public static string GetTemplateForProperty(ModelMetadata propertyMetadata)
        {
            var templateName = propertyMetadata.TemplateHint ?? propertyMetadata.DataTypeName;

            if (templateName == null)
            {
                templateName = TemplateMap.ContainsKey(propertyMetadata.ModelType) ?
                    TemplateMap[propertyMetadata.ModelType] :
                    propertyMetadata.ModelType.Name;
            }

            return "Angular/" + templateName;
        }

        public static IHtmlString AngularAntiForgeryToken(this HtmlHelper helper)
        {
            string cookieToken, formToken;
            AntiForgery.GetTokens(null, out cookieToken, out formToken);

            return helper.Hidden("__RequestVerificationToken", string.Empty, new
            {
                @id = "__RequestVerificationToken",
                data_ng_model = "antiForgeryToken",
                data_ng_init = "antiForgeryToken='" + cookieToken + ":" + formToken + "'"
            });
        }
    }
}