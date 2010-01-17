using FubuMVC.Core.Util;

namespace SimpleWebsite.Core
{
    public static class ModelExtensions
    {
        public static string ToJson(this object model)
        {
            return JsonUtil.ToJson(model);
        }
    }
}