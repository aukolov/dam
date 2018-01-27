using System;
using Dam.Application;
using Dam.Domain;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace Dam.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var bootstrapper = new Bootstrapper())
            {
                try
                {
                    bootstrapper.Start();
                    BuildWebHost(args).Run();
                }
                catch (Exception e)
                {
                    Global.Logger?.Error(e, "Error while running web server.");
                }
                finally
                {
                    Global.Logger?.Dispose();
                }
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog()
                .Build();
    }
}
