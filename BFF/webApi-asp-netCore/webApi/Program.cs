// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;  //IWebHostBuilder includes UseStartup<>()
using Microsoft.Extensions.Hosting;   //ASP.NET Core templates create a .NET Core Generic Host('HostBuilder')   
// using Microsoft.Extensions.Logging;

//https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-3.1

namespace webApi
{
    public class Program
    {//The generic host is typically configured, built and run
        public static void Main(string[] args)
        {
            //Create and configure a "builder object" with "CreateHostBUilder" method
            //Calls Build and Run method to start up
            CreateHostBuilder(args).Build().Run();
        }

        
        //Creates a generic Host using Http workload with "ConfigureWebHostDefaults"
        /*
            If the app uses Entity Framework Core, don't change the name or signature of the CreateHostBuilder method. 
            The Entity Framework Core tools expect to find a CreateHostBuilder method that configures the host without running the app. 
        */
        /*
        * CreateDefaultBuilder implement:
            1. Sets content root path
            2. Loads host configuration
            3. Loads app configuration
            4. Adds logging
            5. Enables scope validation and dependency validation
        */
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //Calls the extension methods assume webBuilder is an instance of IWebHostBuilder
                    webBuilder.UseStartup<Startup>();
                });
    }
}
