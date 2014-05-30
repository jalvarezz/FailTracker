﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace FailTracker.Helpers
{
    public static class JavaScriptHelper
    {
        public static IHtmlString Json(this HtmlHelper helper, object obj)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new JsonConverter[]{
                    new StringEnumConverter()
                },
                StringEscapeHandling = StringEscapeHandling.EscapeHtml
            };

            return MvcHtmlString.Create(JsonConvert.SerializeObject(obj, settings));
        }

        public static IHtmlString AngularEditorForModel(this HtmlHelper helper,
            string modelPrefix)
        {
            return helper.EditorForModel("Angular/Object",
                new { Prefix = modelPrefix });
        }

        public static IHtmlString AngularBindingForModel(this HtmlHelper helper)
        {
            var prefix = (string)(helper.ViewBag.Prefix);

            if (prefix != null)
            {
                prefix = prefix + ".";
            }

            return MvcHtmlString.Create(prefix + helper.CamelCaseIdForModel());
        }

        public static string CamelCaseIdForModel(this HtmlHelper helper)
        {
            var input = helper.IdForModel().ToString();

            if (string.IsNullOrEmpty(input) || !char.IsUpper(input[0]))
            {
                return input;
            }

            var sb = new StringBuilder();

            for (int i = 0; i < input.Length; ++i)
            {
                var flag = i + 1 < input.Length;

                if (i == 0 || !flag || char.IsUpper(input[i + 1]))
                {
                    var ch = char.ToLower(input[i], CultureInfo.InvariantCulture);
                    sb.Append(ch);
                }
                else
                {
                    sb.Append(input.Substring(i));
                    break;
                }
            }

            return sb.ToString();
        }
    }
}