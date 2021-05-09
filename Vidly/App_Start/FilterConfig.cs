using System.Web;
using System.Web.Mvc;

namespace Vidly
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            // even the home page is restricted with this authorization
            filters.Add(new AuthorizeAttribute());
        }
    }
}
