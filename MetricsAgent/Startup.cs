using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentMigrator.Runner;
using MetricsAgent.Cron;
using MetricsAgent.Cron.Job;
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
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

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
            
            // Документация
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
            services.AddSingleton<IDataConnector, SQLLite>();
            // миграции
            var dbConnector = new SQLLite();
            services.AddFluentMigratorCore()
                .ConfigureRunner(
                    rb => rb.AddSQLite()
                        .WithGlobalConnectionString(dbConnector.GetStringConnection())
                        .ScanIn(typeof(Startup).Assembly).For.Migrations()
                ).AddLogging(logger => logger.AddFluentMigratorConsole());
            
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
            
            // Сервисы по рассписанию
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddSingleton<CpuMetricJob>();
            services.AddSingleton<DotNetMetricJob>();
            services.AddSingleton<HddMetricJob>();
            services.AddSingleton<NetworkMetricJob>();
            services.AddSingleton<RamMetricJob>();
            services.AddSingleton(new JobSchedule(jobType: typeof(CpuMetricJob), cronExpression: "0/5 * * * * ?"));
            services.AddSingleton(new JobSchedule(jobType: typeof(DotNetMetricJob), cronExpression: "0/5 * * * * ?"));
            services.AddSingleton(new JobSchedule(jobType: typeof(HddMetricJob), cronExpression: "0/5 * * * * ?"));
            services.AddSingleton(new JobSchedule(jobType: typeof(NetworkMetricJob), cronExpression: "0/5 * * * * ?"));
            services.AddSingleton(new JobSchedule(jobType: typeof(RamMetricJob), cronExpression: "0/5 * * * * ?"));
            services.AddHostedService<QuartzHostedService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMigrationRunner migrationRunner)
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
            
            // запускаем миграции
            migrationRunner.MigrateUp();
        }
    }
}
