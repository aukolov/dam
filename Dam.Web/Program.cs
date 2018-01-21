using Dam.Application;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Dam.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var bootstrapper = new Bootstrapper())
            {
                bootstrapper.Start();
                BuildWebHost(args).Run();
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
