using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FailTracker.Infrastructure.ModelMetadata.Filters
{
    public class WatermarkConventionFilter : IModelMetadataFilter
    {
        public void TransformMetadata(System.Web.Mvc.ModelMetadata metadata, IEnumerable<Attribute> attributes)
        {
            if(!string.IsNullOrEmpty(metadata.DisplayName) &&
                string.IsNullOrEmpty(metadata.Watermark))
            {
                metadata.Watermark = metadata.DisplayName + "...";
            }
        }
    }
}