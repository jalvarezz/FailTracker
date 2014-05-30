using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FailTracker.Infrastructure.ModelMetadata.Attributes
{
    public class Render : System.Attribute
    {
        public bool ShowForEdit { get; set; }

        public Render(bool showForEdit = true) { 
            this.ShowForEdit = showForEdit;
        }
    }

    public class RenderMetadataFilter : IModelMetadataFilter
    {
        public void TransformMetadata(System.Web.Mvc.ModelMetadata metadata, IEnumerable<Attribute> attributes)
        {
            var attribute = attributes.OfType<Render>().FirstOrDefault();

            if (attribute != null)
            {
                metadata.ShowForEdit = ((Render)attribute).ShowForEdit;
            }
        }
    }
}