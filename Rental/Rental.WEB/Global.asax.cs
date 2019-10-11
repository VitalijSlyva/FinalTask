using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Rental.WEB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            IncludeNinject();
        }

        protected void IncludeNinject()
        {
            NinjectModule ninjectModuleBLL = new BLL.Infrastructure.ServiceModuleBLL("IdentityContext", "RentContext");
            NinjectModule ninjectModuleWEB = new WEB.Infrastructure.ServiceModuleWEB();
            var kernel = new StandardKernel(ninjectModuleBLL, ninjectModuleWEB);
            kernel.Unbind<ModelValidatorProvider>();
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}
