using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Opc.Ua;
using Opc.Ua.Configuration;
using OPCFoundationServerLib.Server;
using System;
using TasksLib.Tasks;

namespace Hangfire.WindowsServiceApplication.Tasks
{
    public class Test
    {
        public void Launch()
        {
            Console.Clear();
            ApplicationInstance application = new ApplicationInstance();
            application.ApplicationType = ApplicationType.Server;
            application.ConfigSectionName = "Generic Server";

            try
            {
                application.Start(new UaServer(false)); // if true, loads default nodes from code with simulated data               
                Console.WriteLine("End points: ");
                foreach (var ep in application.ApplicationConfiguration.ServerConfiguration.BaseAddresses)
                {
                    Console.WriteLine("   " + ep);
                }
                ServerTsk.Launch(application, 30000, "SERVER");
            }
            catch (Exception e)
            {
                Utils.Trace("Error: " + e.ToString());
            }
            finally
            {
                application.Stop();
            }
        }
    }
}
