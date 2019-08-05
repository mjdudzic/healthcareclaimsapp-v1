using System;
using System.IO;
using System.Threading;
using ClaimsSubmission.Infrastructure.Persistence;
using DbUp;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HealthcareClaims
{
	public class Program
	{
		private const int ProbesCountMax = 10;

		private static IConfiguration Configuration { get; } = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
			.AddEnvironmentVariables()
			.Build();

		public static void Main(string[] args)
		{
			BuildDatabase();

			var host = CreateWebHostBuilder(args).Build();

			host.Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>();

		private static void BuildDatabase()
		{
			var connectionString = Configuration.GetSection("DB_CONNECTION_STRING").Value;

			CheckDbExists(connectionString, 1);

			var result =
				DeployChanges.To
					.PostgresqlDatabase(connectionString)
					.WithScriptsFromFileSystem(Path.Combine(Directory.GetCurrentDirectory(), "Database/ChangeScripts"))
					.LogToConsole()
					.Build()
					.PerformUpgrade();

			if (!result.Successful)
			{
				Console.WriteLine(result.Error);
			}
		}

		private static void CheckDbExists(string connectionString, int probeCount)
		{
			try
			{
				Console.WriteLine($"Probe {probeCount}/{ProbesCountMax}: Checking db connection...");

				EnsureDatabase.For.PostgresqlDatabase(connectionString);

				Console.WriteLine($"Probe {probeCount}/{ProbesCountMax}: db connection OK");
			}
			catch (Exception e)
			{
				Console.WriteLine($"Probe {probeCount}/{ProbesCountMax}: Cannot connect to db: {e.Message}");
				if (probeCount == ProbesCountMax)
					throw;

				var waitTime = probeCount * 1000 * 5;

				Console.WriteLine($"Probe {probeCount}/{ProbesCountMax}: Wait {waitTime / 1000} seconds and try again ...");

				Thread.Sleep(waitTime);

				CheckDbExists(connectionString, probeCount + 1);
			}
		}
	}
}
