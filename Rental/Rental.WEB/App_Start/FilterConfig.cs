using System.Web;
using System.Web.Mvc;

namespace Rental.WEB
{
    /// <summary>
    /// Filter configuration class
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// Rigister global filters
        /// </summary>
        /// <param name="filters">Collection filters</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
