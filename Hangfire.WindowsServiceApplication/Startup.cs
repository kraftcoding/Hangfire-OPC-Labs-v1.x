using Hangfire.SqlServer;
using Microsoft.Owin;
using OPCFoundation.ServerLib.Constants;
using OPCFoundation.ServerLib.Jobs;
using Owin;
using System;
using System.Configuration;

[assembly: OwinStartup(typeof(Hangfire.WindowsServiceApplication.Startup))]

namespace Hangfire.WindowsServiceApplication
{
    public class Startup
    {
        string filesPath = ConfigurationManager.AppSettings["OPCJobsConfigFilesPath"];

        public void Configuration(IAppBuilder app)
        {
            app.UseErrorPage();
            app.UseWelcomePage("/");

            GlobalConfiguration.Configuration.UseSqlServerStorage(
                "DefaultConnection",
                new SqlServerStorageOptions { QueuePollInterval = TimeSpan.FromSeconds(1) });

            app.UseHangfireDashboard();
            app.UseHangfireServer();

            BackgroundJob.Enqueue(() => ServerJob.Init(InitConstants.SERVER_JOB_FILE, filesPath));
            BackgroundJob.Schedule(() => SuscribeNodesClientJob.Init(InitConstants.SUBSCRIBE_NODES_CLIENT_JOB_FILE, filesPath), TimeSpan.FromMinutes(1));
            RecurringJob.AddOrUpdate(InitConstants.NODE_WRITE_CLIENT_JOB_ID, () => NodeWriteClientJob.Init(InitConstants.NODE_WRITE_CLIENT_JOB_FILE, filesPath), InitConstants.CRON_MINUTELY);
            RecurringJob.AddOrUpdate(InitConstants.NODE_READ_CLIENT_JOB_ID, () => NodeReadClientJob.Init(InitConstants.NODE_READ_CLIENT_JOB_FILE, filesPath), InitConstants.CRON_MINUTELY);
        }
    }
}
