using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FailTracker.Infrastructure.ModelMetadata.Attributes
{
    public class Render : System.Attribute
    {
    }

    public class RenderMetadataFilter : IModelMetadataFilter
    {
        public void TransformMetadata(System.Web.Mvc.ModelMetadata metadata, IEnumerable<Attribute> attributes)
        {
            if (attributes.OfType<Render>().Any())
                metadata.AdditionalValues.Add("Render", true);
        }
    }
}