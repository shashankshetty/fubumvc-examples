using FubuMVC.Core;
using SimpleWebsite.Controllers;
using SimpleWebsite.Core;

namespace SimpleWebsite
{
    public class SimpleWebsiteFubuRegistry : FubuRegistry
    {
        public SimpleWebsiteFubuRegistry()
        {
            IncludeDiagnostics(true);

            Applies.ToThisAssembly();

            Actions
                .IncludeTypesNamed(x => x.EndsWith("Controller"));

            Routes
                .IgnoreControllerNamespaceEntirely();

            JsonOutputIf.CallMatches(action => action.Returns<AjaxResponse>());

            Views.TryToAttach(findViews => findViews.by_ViewModel_and_Namespace_and_MethodName());

            // Note: Outside of a sample application, you would only configure services that Fubu requires within your FubuRegistry
            // Non-Fubu services should be configured through your container in the usual way (StructureMap Registry, etc)
            Services(s => s.AddService<IRepository, FakeRepository>());
        }
    }
}