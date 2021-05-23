using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using FluentMigrator.Runner;
using MetricsAgentsManager.Client;
using MetricsAgentsManager.Client.Interfaces;
using MetricsAgentsManager.Repository;
using MetricsAgentsManager.Cron;
using MetricsAgentsManager.Cron.Job;
using MetricsAgentsManager.DAL.DataConnector;
using MetricsAgentsManager.DAL.Interfaces;
using MetricsAgentsManager.DAL.Mapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Polly;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace MetricsAgentsManager
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
                    Title = "Менеджер агентов метрик", 
                    Description = "Позволяет управлять агентами",
                    Contact = new OpenApiContact
                    {
                        Name = "Заярная Анастасия",
                        Email = string.Empty,
                    },
                    Version = "v1",
                    TermsOfService = new Uri("https://example.com/terms"),
                    License = new OpenApiLicense
                    {
                        Name = string.Empty,
                        Url = new Uri("https://example.com/license"),
                    }
                });
                
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
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
            services.AddSingleton<IAgentsRepository,AgentsRepository>();
            
            // Http
            services.AddHttpClient<IMetricsClient, MetricsClient>()
                // Повторная попытка запроса, в случае не удачи
                .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(1000)));
            
            // Сервисы по расписанию
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
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API сервиса менеджера агентов сборок метрик");
                    c.RoutePrefix = String.Empty;
                });
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