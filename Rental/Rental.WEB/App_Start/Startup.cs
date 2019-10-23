using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(Rental.WEB.App_Start.Startup))]
namespace Rental.WEB.App_Start
{
    /// <summary>
    /// Owin statup class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Owin cofiguration
        /// </summary>
        /// <param name="app">App builder</param>
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions()
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
        }
    }
}