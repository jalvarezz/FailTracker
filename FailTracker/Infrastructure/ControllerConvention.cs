using StructureMap.Graph;
using StructureMap.Pipeline;
using StructureMap.TypeRules;
using StructureMap.Configuration.DSL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FailTracker.Infrastructure
{
    public class ControllerConvention : IRegistrationConvention
    {
        public void Process(Type type, StructureMap.Configuration.DSL.Registry registry)
        {
            if(type.CanBeCastTo(typeof(Controller)) && !type.IsAbstract){
                registry.For(type).LifecycleIs(new UniquePerRequestLifecycle());
            }
        }
    }
}