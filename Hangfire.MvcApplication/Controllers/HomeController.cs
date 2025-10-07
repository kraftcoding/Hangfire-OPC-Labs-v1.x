using OPCFoundation.ServerLib.Constants;
using OPCFoundation.ServerLib.Jobs;
using System;
using System.Configuration;
using System.Web.Mvc;

namespace Hangfire.MvcApplication.Controllers
{
    public class HomeController : Controller
    {
        string filesPath = ConfigurationManager.AppSettings["OPCJobsConfigFilesPath"];

        public ActionResult Index()
        {
            return View((object)TextBuffer.ToString());
        }

        public ActionResult Buffer()
        {
            return Content(TextBuffer.ToString());
        }


        [HttpPost]
        public ActionResult StartServer()
        {
            try
            {
                BackgroundJob.Enqueue(() => ServerJob.Init(InitConstants.SERVER_JOB_FILE, filesPath));
                TextBuffer.WriteLine(string.Format("Background {0} job has been started.", ServerJob.JobName));
            }
            catch (Exception ex)
            {
                TextBuffer.WriteLine(string.Format("{0}", ex.Message));
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult StopServer()
        {
            try
            {
                ServerJob.Stop();
                TextBuffer.WriteLine(string.Format("Background {0} job has been stopped.", ServerJob.JobName));
            }
            catch (Exception ex)
            {
                TextBuffer.WriteLine(string.Format("{0}", ex.Message));
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult StartSuscriber()
        {
            try
            {
                BackgroundJob.Schedule(() => SuscribeNodesClientJob.Init(InitConstants.SUBSCRIBE_NODES_CLIENT_JOB_FILE, filesPath), TimeSpan.FromMinutes(1));
                TextBuffer.WriteLine(string.Format("Background {0} job has been started.", SuscribeNodesClientJob.JobName));
            }
            catch (Exception ex)
            {
                TextBuffer.WriteLine(string.Format("{0}", ex.Message));
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult StopSuscriber()
        {
            try
            {
                SuscribeNodesClientJob.Stop();
                TextBuffer.WriteLine(string.Format("Background {0} job has been stopped.", SuscribeNodesClientJob.JobName));
            }
            catch (Exception ex)
            {
                TextBuffer.WriteLine(string.Format("{0}", ex.Message));
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddWriter()
        {
            try
            {
                RecurringJob.AddOrUpdate(InitConstants.NODE_WRITE_CLIENT_JOB_ID, () => NodeWriteClientJob.Init(InitConstants.NODE_WRITE_CLIENT_JOB_FILE, filesPath), InitConstants.CRON_MINUTELY);
                TextBuffer.WriteLine(string.Format("Recurring {0} job has been started.", NodeWriteClientJob.JobName));
            }
            catch (Exception ex)
            {
                TextBuffer.WriteLine(string.Format("{0}", ex.Message));
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddReader()
        {
            try
            {
                RecurringJob.AddOrUpdate(InitConstants.NODE_READ_CLIENT_JOB_ID, () => NodeReadClientJob.Init(InitConstants.NODE_READ_CLIENT_JOB_FILE, filesPath), InitConstants.CRON_MINUTELY);
                TextBuffer.WriteLine(string.Format("Recurring {0} job has been started.", NodeWriteClientJob.JobName));
            }
            catch (Exception ex)
            {
                TextBuffer.WriteLine(string.Format("{0}", ex.Message));
            }

            return RedirectToAction("Index");
        }
    }
}