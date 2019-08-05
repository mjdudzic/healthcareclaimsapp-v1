using ClaimsSubmission;
using ClaimsVetting;
using Hangfire;
using Hangfire.Console;
using Hangfire.PostgreSql;
using HealthcareClaimsWithModules.Application.Services;
using HealthcareClaimsWithModules.Filters;
using Infrastructure.Minio;
using Infrastructure.Minio.Configuration;
using Infrastructure.Minio.Interfaces;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HealthcareClaimsWithModules
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
			AddDbContexts(services);
			AddHangfire(services);
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			AddHangfireMiddleware(app);

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseMvc();
		}

		private void AddDbContexts(IServiceCollection services)
		{
			//services.AddDbContext<BatchesContext>(opt =>
			//	opt.UseInMemoryDatabase("Batches"));

			services
				.AddEntityFrameworkNpgsql()
				.AddDbContext<BatchesContext>(
					options =>
					{
						options.UseNpgsql(Configuration["DB_CONNECTION_STRING"]);
						options.EnableDetailedErrors();
					});

			services.AddTransient<IBatchSubmissionService, BatchSubmissionService>();
			services.AddTransient<IBatchSubmissionFeedbackService, BatchSubmissionFeedbackService>();
			services.AddTransient<IBatchVettingService, BatchVettingService>();

			var objectsStorageServiceConfiguration = new ObjectsStorageServiceConfiguration();
			Configuration.Bind("ObjectsStorage", objectsStorageServiceConfiguration);
			services.AddSingleton<IObjectsStorageServiceConfiguration>(objectsStorageServiceConfiguration);
			services.AddTransient<IObjectsStorageService, ObjectsStorageService>();
			services.AddTransient<IMinioClientFactory, MinioClientFactory>();
		}

		private void AddHangfire(IServiceCollection services)
		{
			services.AddHangfire(config =>
			{
				config.UsePostgreSqlStorage(Configuration["DB_CONNECTION_STRING"]);

				config.UseConsole();
			});
		}

		private void AddHangfireMiddleware(IApplicationBuilder app)
		{
			app.UseHangfireServer();

			app.UseHangfireDashboard("/dashboard", new DashboardOptions
			{
				Authorization = new[] { new FakeDashboardAuthFilter() }
			});
		}
	}
}
