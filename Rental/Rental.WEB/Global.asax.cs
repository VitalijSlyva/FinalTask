using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Rental.WEB
{
    /// <summary>
    /// Main application class.
    /// </summary>
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Initialization.
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            IncludeNinject();
        }

        /// <summary>
        /// Register Ninject dependency. 
        /// </summary>
        protected void IncludeNinject()
        {
            NinjectModule ninjectModuleBLL = new BLL.Infrastructure
                .ServiceModuleBLL("IdentityContext", "RentContext","LogContext");

            NinjectModule ninjectModuleWEB = new WEB.Infrastructure.ServiceModuleWEB();

            var kernel = new StandardKernel(ninjectModuleBLL, ninjectModuleWEB);
            kernel.Unbind<ModelValidatorProvider>();

            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}
