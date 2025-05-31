using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;



namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder
                .UseStartup<Startup>()
                .UseKestrel(options =>
                {
                    options.ListenLocalhost(5001, listenOptions =>
                    {
                        listenOptions.UseHttps("SSL/server.pfx", "c1nna1v410%");
                    });
                });
        });
        

    }
    
}