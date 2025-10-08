using Hangfire.OPC.Configuration;
using Hangfire.OPC.Configuration.Logs;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Hangfire.MvcApplication.Startup))]

namespace Hangfire.MvcApplication
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage("DefaultConnection");

            app.UseHangfireDashboard();
            app.UseHangfireServer();

            // Add a filter to log everything

            GlobalJobFilters.Filters.Add(new LogEverythingAttribute());
        }
    }
}
