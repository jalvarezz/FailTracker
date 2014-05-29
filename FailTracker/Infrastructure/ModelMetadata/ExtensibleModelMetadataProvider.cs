using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FailTracker.Infrastructure.ModelMetadata
{
    public class ExtensibleModelMetadataProvider : DataAnnotationsModelMetadataProvider
    {
        private readonly IModelMetadataFilter[] _metadataFilters;

        public ExtensibleModelMetadataProvider(IModelMetadataFilter[] metadataFilters)
        {
            _metadataFilters = metadataFilters;
        }

        protected override System.Web.Mvc.ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            var metadata = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);

            foreach (var item in _metadataFilters)
            {
                item.TransformMetadata(metadata, attributes);
            }

            return metadata;
        }
    }
}