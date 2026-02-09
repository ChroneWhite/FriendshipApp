using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using API.Extensions;
using API.Middleware;
using API.SignalR;
using static Microsoft.AspNetCore.Routing.IEndpointRouteBuilder;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.HttpOverrides;

namespace API
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationServices(_config);
            services.AddControllers();
            services.AddIdentityServices(_config);
            services.AddCors();
            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPIv5", Version = "v1" });
                });
            services.AddSignalR();
            
        
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    
    app.UseDefaultFiles(new DefaultFilesOptions
    {
        FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "wwwroot"))
    });

    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "wwwroot"))
    });

    app.UseMiddleware<ExceptionMiddleware>();

    app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedProto,
        KnownNetworks = { },
        KnownProxies = { }
    });


    app.UseRouting();

    app.UseCors(x => x
        .AllowAnyHeader()
        .AllowCredentials()
        .AllowAnyMethod()
        .WithOrigins("http://localhost:8080"));
        
    app.UseAuthentication();
    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        endpoints.MapHub<PresenceHub>("hubs/presence");
        endpoints.MapHub<MessageHub>("hubs/message");
        endpoints.MapFallbackToController("Index", "Fallback");
        
    });

    

}
}
}