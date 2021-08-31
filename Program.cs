using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Commander
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // var host = CreateHostBuilder(args).Build();
            CreateHostBuilder(args).Build().Run();

            // using (var scope = host.Services.CreateScope())
            // {
            //     var data = scope.ServiceProvider.GetRequiredService<IDataService>();
            // }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
