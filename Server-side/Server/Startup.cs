using Business;
using Game;
using Ghost;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;

namespace Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add cors to allow the client make calls.
            services.AddCors();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Ghost Api", Version = "v1" });
            });

        
            services.AddSingleton<IResource<IEnumerable<string>>, Resource>();
            services.AddSingleton<IHeuristic<string, int>, GhostHeuristic>();
            services.AddSingleton<IDecisionMaker<string, int>, Minimax<string, int>>();
            services.AddSingleton<ITreeBuilder<string>, GhostTree>();
            services.AddSingleton<ILogic<string>, GhostLogic>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Add cors to allow the client make calls.
            app.UseCors(
                options => options
                .AllowAnyOrigin()
            );

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ghost API V1");
            });
        }
    }
}
