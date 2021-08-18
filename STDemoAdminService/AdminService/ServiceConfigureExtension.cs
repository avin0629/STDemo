using AdminService.V1.Repository;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AdminService
{
	/// <summary>
	/// ServiceConfigureExtension
	/// </summary>
	public static class ServiceConfigureExtension
	{
		/// <summary>
		/// Add cors policy to service.
		/// </summary>
		/// <param name="services"></param>
		/// <param name="policyName"></param>
		public static void AddCorsPolicy(this IServiceCollection services, string policyName)
		{
			
		}

		/// <summary>
		/// Add swagger configuration to service.
		/// </summary>
		/// <param name="services"></param>
		public static void AddSwaggerConfiguration(this IServiceCollection services)
		{
			services.AddSwaggerGen(options =>
			{
				// resolve the IApiVersionDescriptionProvider service
				// note: that we have to build a temporary service provider here because one has not been created yet
				var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

				// add a swagger document for each discovered API version
				// note: you might choose to skip or document deprecated API versions differently
				foreach (var description in provider.ApiVersionDescriptions)
				{
					options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
				}

				// add a custom operation filter which sets default values
				options.OperationFilter<SwaggerDefaultValues>();

				// integrate xml comments
				options.IncludeXmlComments(XmlCommentsFilePath);
			});
		}

		/// <summary>
		/// //Add dependency injection for the service.
		/// </summary>
		public static void AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton<ILoggerFactory, LoggerFactory>();
			services.AddScoped<IPersonRepository, PersonRepository>();
		}

		/// <summary>
		/// Inject external services dependency client.
		/// </summary>
		/// <param name="services"></param>
		/// <param name="configuration"></param>
		public static void AddServicesDependency(this IServiceCollection services, IConfiguration configuration)
		{
			
		}


		/// <summary>
		/// Extension method to create Api info version
		/// </summary>
		/// <param name="description"></param>
		/// <returns></returns>
		private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
		{
			var info = new OpenApiInfo()
			{
				Title = $"ST Admin Demo Service Business REST Web API {description.ApiVersion}",
				Version = description.ApiVersion.ToString(),
				Description = "ST - Admin Demo business and backend service for eWealthManager.",
				Contact = new OpenApiContact() { Name = "Avinash Ghawali", Email = "avighawali@gmail.com" },
				//TermsOfService = new Uri(""),
				//License = new OpenApiLicense() { Name = "", Url = new Uri("") }
			};

			if (description.IsDeprecated)
			{
				info.Description += " This API version has been deprecated.";
			}

			return info;
		}

		/// <summary>
		/// Extension method to include XML Comments.
		/// </summary>
		private static string XmlCommentsFilePath
		{
			get
			{
				//Determine base path for the application.
				var basePath = AppContext.BaseDirectory;

				var assemblyName = Assembly.GetEntryAssembly().GetName().Name;
				var fileName = Path.GetFileName($"{assemblyName}.xml");

				return Path.Combine(basePath, fileName);
			}
		}
	}
}
