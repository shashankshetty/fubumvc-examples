using FubuMVC.Core.Registration.Conventions;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Registration.Routes;

namespace SimpleWebsite.EndPoints
{
    public class EndPointUrlPolicy : IUrlPolicy
    {
        public bool Matches(ActionCall call)
        {
            return call.HandlerType.Name.EndsWith("Endpoint");
        }

        public IRouteDefinition Build(ActionCall call)
        {
            var routeDefinition = call.ToRouteDefinition();
            routeDefinition.Append(call.HandlerType.Namespace.Replace(GetType().Namespace + ".", string.Empty).ToLower());
            routeDefinition.Append(call.HandlerType.Name.Replace("Endpoint", string.Empty).ToLower());
            return routeDefinition;
        }
    }
}