using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace BBF
{
   public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            services.AddOcelot();
            services.AddSwaggerForOcelot(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowAll");
            
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwaggerForOcelotUI(Configuration, opt =>
            {
                opt.PathToSwaggerGenerator = "/swagger/docs";
            });
            app.UseOcelot().Wait();
        }
    }
}
