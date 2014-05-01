using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FailTracker.Infrastructure
{
    public static class HttpContextBaseContainerExtensions
    {
        private static readonly string _nestedContainerKey = "_Container";

        public static void SetContainer(this HttpContextBase context, IContainer container)
        {
            context.Items[_nestedContainerKey] = container;
        }

        public static IContainer GetContainer(this HttpContextBase context)
        {
            return context.Items[_nestedContainerKey] as IContainer;
        }
    }
}