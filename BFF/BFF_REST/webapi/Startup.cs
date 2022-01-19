using System.Collections.Immutable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;

using WebApi.Data;
using Microsoft.EntityFrameworkCore;
using Minio;
using AutoMapper;
using WebApi.Models;
using WebApi.Helpers;
using System.Net;

namespace WebApi
{
    public class Startup
    {
        private readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //CORS problem
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins, builder =>
                {
                    builder.WithOrigins(Configuration["HostAllowCors:Url1"], Configuration["HostAllowCors:Url2"]).AllowAnyHeader();
                });
            });


            services.AddControllers();
            services.AddControllers().AddNewtonsoftJson(s =>
            {
                s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });


             // configure strongly typed settings object
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            //add database context to connect database and get data from db
            services.AddDbContext<DeviceStatusContext>(opt =>
                opt.UseMySQL(Configuration.GetConnectionString("DeviceStatusDbConnection")));

            services.AddDbContext<DeviceDataContext>(opt =>
                opt.UseMySQL(Configuration.GetConnectionString("DeviceMsgDbConnection")));

            services.AddDbContext<UserInfoContext>(opt =>
                opt.UseMySQL(Configuration.GetConnectionString("DeviceMsgDbConnection")));

            //Inform automapper what classes can mapped to
            //Each AppDomain can only be configured once
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
            Console.WriteLine(Configuration["Minio:userName"]+" "+Configuration["Minio:pwd"]);
            //build single minioClient instance 
            services.AddSingleton(new MinioClient(Configuration["Minio:endpoint"],Configuration["Minio:userName"],Configuration["Minio:pwd"]).WithSSL());
            ServicePointManager.ServerCertificateValidationCallback +=
                        (sender, certificate, chain, sslPolicyErrors) => true;
            //add Dependency injection(create instance by services)
            services.AddScoped<IDeviceStatusRepo, SqlDeviceStatusRepo>();
            services.AddScoped<IDeviceMsgInfoRepo, SqlDeviceMsgInfoRepo>();
            services.AddScoped<IUserInfoRepo, SqlUserInfoRepo>();
            services.AddScoped<IVideoInfo, MinioVideoInfo>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            //add the function of checking if token is existed and valid
            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
