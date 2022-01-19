using System.Collections.Immutable;
using System;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
// using Microsoft.AspNetCore.HttpsPolicy;
// using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;   //IConfiguration
using Microsoft.Extensions.DependencyInjection;    // MvcServiceCollectionExtensions includes AddControllers, AddMvc and so on.
using Microsoft.Extensions.Hosting;
// using Microsoft.Extensions.Logging;

using AutoMapper;
using webApi.Data;
using webApi.Models;
using webApi.Helper;

//https://docs.microsoft.com/en-us/aspnet/core/fundamentals/static-files?view=aspnetcore-3.1

namespace webApi
{
    public class Startup
    {
        /*
            This class configures services and the app's request pipeline. It includes
                1.ConfigureServices method (configure the app's services)
                    1.1 A service is a reusable component that provides app functionality.
                    1.2 DI
                    1.3 ApplicationServices
                2.Configure method (create the app's request processing pipeline.)
        */

        private readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        //IConfiguration represents a set of key/value application configuration properties.
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Enable Cross-Origin Request
            services.AddCors(opt =>
            {
                opt.AddPolicy(name: MyAllowSpecificOrigins, builder =>
                 {
                    //Allows to add multiple ip
                    builder.WithOrigins(Configuration["HostAllowCors:localhost"]).AllowAnyHeader(); //, Configuration["HostAllowCors:server"]).AllowAnyHeader();
                });
            });

            //Adds services for controllers to the specified IServiceCollection.( not for pages or views)
            services.AddControllers();

            //To enable JSON Patch support
            services.AddControllers().AddNewtonsoftJson();
            // services.AddControllers().AddNewtonsoftJson(s =>
            // {
            //     s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            // });

            //Configure strongly typed settings object
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            //Builds Dbcontext with database
            //SelectCrs Context -> database
            services.AddDbContext<SelectCrsContext>(opt => opt.UseMySql(Configuration.GetConnectionString("SelectCourseConnection")));

            //UserInfo Context -> database
            services.AddDbContext<UserInfoContext>(opt => opt.UseMySql(Configuration.GetConnectionString("SelectCourseConnection")));

            //Inform automapper what classes can mapped to
            //Each AppDomain can only be configured once
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //DI
            services.AddScoped<ICourseRepo, SqlCourseRepo>();
            services.AddScoped<IUserInfoRepo, SqlUserInforepo>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //Specify how the app responds to HTTP requests
        //Hosting creates an IApplicationBuilder and passes it directly to Configure
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //Displays detailed information about request exceptions.
                app.UseDeveloperExceptionPage();
            }

            //Redirect HTTP requests to HTTPS
            app.UseHttpsRedirection();

            //adds route matching to the middleware pipeline. 
            //This middleware looks at the set of endpoints defined in the app, 
            //and selects the best match based on the request.
            app.UseRouting();

            //CROS
            app.UseCors(MyAllowSpecificOrigins);

            //Adds the AuthorizationMiddleware to enables authorization capabilities
            app.UseAuthorization();

            //add the function of checking if token is existed and valid
            app.UseMiddleware<JwtMiddleware>();

            //Runs the delegate associated with the selected endpoint.
            app.UseEndpoints(endpoints =>
            {
                //1.Transform all Controller and Action in project into endpoints and put it in RouteOptions object which middleware configure 
                //2.Register EndpointMiddleware to http pipeline
                endpoints.MapControllers();
            });
        }
    }
}
