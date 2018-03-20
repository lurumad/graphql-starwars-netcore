using AutoMapper;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StarWars.Api.Models;
using StarWars.Core.Data;
using StarWars.Data.EntityFramework;

namespace StarWars.Api
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
            services
                .AddAutoMapper(typeof(Startup))
                .AddDbContextPool<StarWarsContext>(options =>
                {
                    options.UseSqlServer(
                        Configuration.GetConnectionString("DefaultConnection"),
                        sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly("StarWars.Data");
                        });
                    options.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
                })
                .AddScoped<IDocumentExecuter,DocumentExecuter>()
                .AddScoped<StarWarsQuery>()
                .AddScoped<DroidType>()
                .AddScoped<HumanType>()
                .AddScoped<CharacterInterface>()
                .AddScoped<EpisodeEnum>()
                .AddScoped<ICharacterRepository, Data.EntityFramework.Repositories.CharacterRepository>()
                .AddMvc();

                var sp = services.BuildServiceProvider();
                services.AddScoped<ISchema>(_ => new StarWarsSchema(type => (GraphType)sp.GetService(type)) { Query = sp.GetService<StarWarsQuery>() });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
