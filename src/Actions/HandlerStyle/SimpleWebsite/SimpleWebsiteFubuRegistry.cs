using System;
using FubuMVC.Core;
using SimpleWebsite.Core;
using SimpleWebsite.Handlers;

namespace SimpleWebsite
{
    public class SimpleWebsiteFubuRegistry : FubuRegistry
    {
        public SimpleWebsiteFubuRegistry()
        {
            IncludeDiagnostics(true);

            Applies.ToThisAssembly();

            Actions
                .IncludeTypes(t => t.Namespace.StartsWith(typeof(HandlerUrlPolicy).Namespace) && t.Name.EndsWith("Handler"))
                .IncludeMethods(action => action.Method.Name == "Execute");

            Routes.UrlPolicy<HandlerUrlPolicy>();

            JsonOutputIf.CallMatches(action => action.Returns<AjaxResponse>());

            Views.TryToAttach(findViews => findViews.by_ViewModel_and_Namespace());

            // Note: Outside of a sample application, you would only configure services that Fubu requires within your FubuRegistry
            // Non-Fubu services should be configured through your container in the usual way (StructureMap Registry, etc)
            Services(s => s.AddService<IRepository, FakeRepository>());
        }
    }
}