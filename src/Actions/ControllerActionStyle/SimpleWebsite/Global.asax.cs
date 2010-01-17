using FubuMVC.Core;
using FubuMVC.StructureMap.Bootstrap;

namespace SimpleWebsite
{
    public class Global : FubuStructureMapApplication
    {
        public override FubuRegistry GetMyRegistry()
        {
            return new SimpleWebsiteFubuRegistry();
        }
    }
}