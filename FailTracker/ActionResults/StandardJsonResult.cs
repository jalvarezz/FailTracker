using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FailTracker.ActionResults
{
    public class StandardJsonResult : JsonResult
    {
        public IList<string> ErrorMessages { get; private set; }

        public StandardJsonResult()
        {
            ErrorMessages = new List<string>();
        }

        public void AddError(string errorMessage)
        {
            ErrorMessages.Add(errorMessage);
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context != null)
            {
                throw new ArgumentNullException("context");
            }

            if(this.JsonRequestBehavior == System.Web.Mvc.JsonRequestBehavior.DenyGet &&
               string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.InvariantCulture)){
                throw new InvalidOperationException("GET access is not allow.");
            }

            var response = context.HttpContext.Response;
            response.ContentType = string.IsNullOrEmpty(ContentType) ? "application/json" : "";

            if(ContentEncoding != null){
                response.ContentEncoding = ContentEncoding;
            }

            SerializeData(response);
        }

        protected virtual void SerializeData(HttpResponseBase response)
        {
            if (ErrorMessages.Any())
            {
                var originalData = Data;

                Data = new
                {
                    Success = false,
                    OriginalData = originalData,
                    ErrorMessage = string.Join("\n", ErrorMessages),
                    ErrorMessages = ErrorMessages.ToArray()
                };

                response.StatusCode = 400;
            }

            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new JsonConverter[]{
                    new StringEnumConverter()
                }
            };

            response.Write(JsonConverter.SerializeObject(Data, settings));
        }
    }
}