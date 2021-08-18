using AdminService.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdminService
{
	public class Program
	{
		public static void Main(string[] args)
		{
			string currentEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

			IConfigurationBuilder configBuilder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", false, reloadOnChange: true);

			if (currentEnvironment?.Equals("Development", StringComparison.OrdinalIgnoreCase) == true)
			{
				configBuilder.AddJsonFile($"appsettings.{currentEnvironment}.json", optional: false);
			}

			IConfigurationRoot config = configBuilder.Build();
			string loggingMethod = config.GetValue<string>("LoggingMethod") ?? nameof(LoggingMethod.NLog);
			LogManager.Configuration = new NLogLoggingConfiguration(config.GetSection(nameof(LoggingMethod.NLog)));
			Logger logger = NLogBuilder.ConfigureNLog(LogManager.Configuration).GetCurrentClassLogger();

			try
			{
				logger.Debug("init main");
				CreateHostBuilder(args).Build().Run();
			}
			catch (Exception ex)
			{
				//NLog: catch setup errors
				logger.Error(ex, "Stopped program because of exception");
				throw;
			}
			finally
			{
				// Ensure to flush and stop internal timers/threads before application-exit.
				NLog.LogManager.Shutdown();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="args"></param>
		/// <param name="loggingMethod"></param>
		/// <returns></returns>
		public static IHostBuilder CreateHostBuilder(string[] args, string loggingMethod = "NLog")
		{
			var hostBuilder = Host.CreateDefaultBuilder(args)
									.ConfigureWebHostDefaults(webBuilder =>
									{
										webBuilder.UseStartup<Startup>();
									});

			if (loggingMethod == nameof(LoggingMethod.NLog))
			{
				hostBuilder = hostBuilder.UseNLog();
			}

			return hostBuilder;
		}
	}
}
