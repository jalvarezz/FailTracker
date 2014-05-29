using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace FailTracker.Infrastructure.ModelMetadata.Filters
{
    public class LabelConventionFilter : IModelMetadataFilter
    {
        public void TransformMetadata(System.Web.Mvc.ModelMetadata metadata, IEnumerable<Attribute> attributes)
        {
            if (!string.IsNullOrEmpty(metadata.PropertyName) && string.IsNullOrEmpty(metadata.DisplayName))
            {
                metadata.DisplayName = GetStringWithSpace(metadata.PropertyName);
            }
        }

        private string GetStringWithSpace(string input)
        {
            return Regex.Replace(input,
                "(?<!^)" +
                "(" +
                "  [A-Z][a-z] |" +
                "  (?<=[a-z])[A-Z] |" +
                "  (?<![A-Z])[A-Z]$" +
                ")", " $1", RegexOptions.IgnorePatternWhitespace);
        }
    }
}