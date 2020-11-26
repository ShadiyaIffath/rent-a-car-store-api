using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hangfire;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Model.DatabaseContext;
using Model.Enums;
using Model.Mapper;
using Model.Models.MailService;
using Model.Repositories;
using Model.Repositories.Interfaces;
using Newtonsoft.Json.Serialization;
using ProjectAPI.Controllers;
using ProjectAPI.Filter;
using ProjectAPI.Interfaces;
using ProjectAPI.Services;
using ProjectAPI.Services.Interfaces;

namespace ProjectAPI
{
    public class Startup
    {
        public static IConfigurationRoot configuration;
        private TokenValidationParameters tokenValidationParameters;
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                 .AddEnvironmentVariables();

            configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("EnableCORS", builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
            services.AddControllers()
                .AddNewtonsoftJson(jsonOptions =>
                {
                    jsonOptions.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });
            services.Configure<string>(Startup.configuration.GetSection(""));
            var systemConnectionString = Startup.configuration["AppSettings:ConnectionString"];
            var key = Startup.configuration["AppSettings:JwtKey"];

            services.AddDbContext<ClientDbContext>(options =>
                options.UseSqlServer(systemConnectionString,
                    optionsBuilder =>
                        optionsBuilder.MigrationsAssembly("Model")));

            services.AddAuthentication(x =>
           {
               x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
               x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
           }).AddJwtBearer(x =>
           {
               x.RequireHttpsMetadata = false;
               x.SaveToken = true;
               tokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                   ValidateIssuer = false,
                   ValidateAudience = false
               };
               x.TokenValidationParameters = tokenValidationParameters;
               x.Events = new JwtBearerEvents
               {
                   OnAuthenticationFailed = context =>
                   {
                       if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                       {
                           context.Response.Headers.Add("Token-Expired", "true");
                       }
                       return Task.CompletedTask;
                   }
               };
           });

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddSingleton<IJwtAuthenticationManager>(new JwtAuthenticationManager(key));
            services.AddTransient<IMailService, Services.MailService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IEquipmentService, EquipmentService>();
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IInquiryService, InquiryService>();
            services.AddScoped<IDMVService, DMVService>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IVehicleBookingRepository, VehicleBookingRepository>();
            services.AddScoped<IEquipmentRepository, EquipmentRepository>();
            services.AddScoped<IEquipmentBookingRepository, EquipmentBookingRepository>();
            services.AddScoped<IInquiryRepository, InquiryRepository>();
            services.AddScoped<IDMVRepository, DMVRepository>();

            services.AddHangfire(options =>
            {
                options.UseSqlServerStorage(systemConnectionString);
            });
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

            app.UseCors("EnableCORS");

            app.UseAuthentication();

            app.UseAuthorization();

            var options = new DashboardOptions
            {
                Authorization = new IDashboardAuthorizationFilter[]
        {
            new HangfireAuthorizationFilter(this.tokenValidationParameters, nameof(AccTypes.admin))
        }
            };
            app.UseHangfireDashboard("/main/admin/hangfire", options);
            app.UseHangfireServer();
            RecurringJob.AddOrUpdate<DMVService>(s => s.GetLicenses(), "1 0 * * *", TimeZoneInfo.Local) ;

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
