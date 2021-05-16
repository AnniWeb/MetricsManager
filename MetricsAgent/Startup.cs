using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MetricsAgent.DAL.DataConnector;
using MetricsAgent.DAL.Interfaces;
using MetricsAgent.DAL.Mapper;
using MetricsAgent.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace MetricsAgent
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
            // Контроллеры
            services.AddControllers();
            
            // Докумеентация
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Менеджер метрик", 
                    Description = "Позволяет отслеживать и анализировать параметры системы",
                    Contact = new OpenApiContact
                    {
                        Name = "Заярная Анастасия"
                    },
                    Version = "v1"
                });
            });

            // БД
            services.AddSingleton<IDataConnector,SQLLite>();
            
            // Мапперы
            var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);
            
            // Репозитории
            services.AddSingleton<ICPUMetricsRepository,CPUMetricsRepository>();
            services.AddSingleton<IDotNetMetricsRepository,DotNetMetricsRepository>();
            services.AddSingleton<IHddMetricsRepository,HddMetricsRepository>();
            services.AddSingleton<INetworkMetricsRepository,NetworkMetricsRepository>();
            services.AddSingleton<IRamMetricsRepository,RamMetricsRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger(c => c.SerializeAsV2 = true);
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MetricsAgent v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}