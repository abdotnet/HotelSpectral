using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hangfire;
using HotelSpectral.Data;
using HotelSpectral.Domain.Interfaces;
using HotelSpectral.Domain.Models;
using HotelSpectral.Domain.Services;
using HotelSpectral.Domain.Store;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HotelSpectral.Domain.Infrastructure
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class Middleware
    {
        private readonly RequestDelegate _next;

        public Middleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {

            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddlewareClassTemplate(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Middleware>();
        }

        public static void AddDatabase(this IServiceCollection services, IConfiguration _config)
        {
            var connectionString = _config.GetConnectionString("conMySql");

            MigrateAssembly();

            services.AddDbContextPool<HotelSpectralContext>(options => options.UseMySql(connectionString));

            //Add Scoped  Hotelspectral ... 
            services.AddScoped<DbContext, HotelSpectralContext>();
           // services.AddTransient<IUserRepository, UserRepository>();

        }
        
        private static void MigrateAssembly()
        {
            var migrationAssembly = typeof(HotelSpectralContext).GetTypeInfo().Assembly.GetName().Name;
        }

        // Application Identity configuration ..
        public static void AddApplicationIdentity(this IServiceCollection services, IConfiguration config)
        {
            _ = services.AddIdentity<UserModel, string>(o =>
              {
                // configure identity options
                o.Password.RequireDigit = false;
                  o.Password.RequireLowercase = false;
                  o.Password.RequireUppercase = false;
                  o.Password.RequireNonAlphanumeric = false;
                  o.Password.RequiredLength = 6;
              }).AddDefaultTokenProviders();
        }

        public static void InitializeAppServices(this IServiceCollection services)
        {

            // User store & roles  .. 
            services.AddScoped<IUserStore<UserModel>, UserStore>();
            services.AddScoped<IRoleStore<string>, RoleStore>();

            //// Add transient app services  items

            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<IReservationService, ReservationService>();
            services.AddTransient<IGuestService, GuestService>();
            //IGuestService
        }

        public static void HangFireService(this IServiceCollection services, IConfiguration _config)
        {
            services.AddHangfire(_ => _.UseSqlServerStorage(_config.GetConnectionString("conString")));

        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
        }


        /// <summary>
        /// Configure Jwt authentication service  .. 
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureJwtAuthService(this IServiceCollection services, IConfiguration _config, Microsoft.Extensions.Hosting.IHostingEnvironment _env)
        {


            var keyByteArray = Encoding.ASCII.GetBytes(_config["Tokens:key"]);
            var signingKey = new SymmetricSecurityKey(keyByteArray);

            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!  
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                // Validate the JWT Issuer (iss) claim  
                ValidateIssuer = true,
                ValidIssuer = _config["Tokens:Issuer"],

                // Validate the JWT Audience (aud) claim  
                ValidateAudience = true,
                ValidAudience = _config["Tokens:Audience"],

                // Validate the token expiry  
                ValidateLifetime = LifetimeValidator(int.Parse(_config["Tokens:AccessExpireMinutes"])),

                ClockSkew = TimeSpan.Zero
            };

            //add permission enable cross-origin requests (CORS) from angular
            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowAnyOrigin();
            corsBuilder.AllowCredentials();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", corsBuilder.Build());

            });


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = tokenValidationParameters;
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                          .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                          .RequireAuthenticatedUser().Build());
            });

            services.AddAutoMapper();


            services.AddSingleton<Microsoft.Extensions.Hosting.IHostingEnvironment>(_env);
            services.AddSingleton<IConfiguration>(_config);

            // Register app services  ..
            services.InitializeAppServices();
            //services.AddMvc();
            services.AddMvc(opt =>
            {
                // opt.ModelBinderProviders.Insert(0, new UserModelBinderProvider());

                if (_config["DisableSSL"] != "true")
                {
                    // opt.Filters.Add(new RequireHttpsAttribute());
                }

            }).AddJsonOptions(opt =>
            {
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
                opt.SerializerSettings.SerializationBinder = new DefaultSerializationBinder();
                opt.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;

            });
        }

        public static void Logger(this IApplicationBuilder app, ILoggerFactory loggerFactory, IConfiguration _config)
        {
            if (loggerFactory != null)
            {
              //  loggerFactory.AddConsole(_config.GetSection("Logging"));
            }
           // loggerFactory.AddDebug();

            ////FileLoggerExtensions
            loggerFactory.AddFile("logs/Hotelspectral-{Date}.txt");
        }

        public static void ActivateReactMvc(this IApplicationBuilder app, IHostingEnvironment env, IConfiguration configuration)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            //app.UserCorsMiddleWare();

            app.UseDefaultFiles();
            app.UseStaticFiles();
           // app.UseAuthentication();


            // app.UseMvc();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                          name: "default",
                          template: "{controller=}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                          name: "spa-fallback",
                          defaults: new { controller = "Home", action = "Index" });
            });

        }
        private static bool LifetimeValidator(int expires)
        {
            if (expires > 0)
            {
                if (DateTime.UtcNow.Minute < expires) return true;
            }
            return false;
        }


    }

}
