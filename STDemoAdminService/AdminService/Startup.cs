using AdminService.Filters;
using AdminService.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AdminService
{
	public class Startup
	{
		/// <summary>
		/// Service Startup.
		/// </summary>
		/// <param name="configuration"></param>
		/// <param name="hostContext"></param>
		public Startup(IConfiguration configuration, IWebHostEnvironment hostContext)
		{
			Configuration = configuration;
			HostContext = hostContext;
		}

		/// <summary>
		/// IConfiguration
		/// </summary>
		public IConfiguration Configuration { get; }

		/// <summary>
		/// IWebHostEnvironment
		/// </summary>
		public IWebHostEnvironment HostContext { get; }

		/// <summary>
		/// This method gets called by the runtime. Use this method to add services to the container.
		/// </summary>
		/// <param name="services"></param>
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();

			services.AddDependencyInjection(Configuration);

			// add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
			// note: the specified format code will format the version as "'v'major[.minor][-status]"
			services.AddVersionedApiExplorer(options =>
			{
				options.GroupNameFormat = "'v'VVV";

				// note: this option is only necessary when versioning by url segment. the SubstitutionFormat
				// can also be used to control the format of the API version in route templates
				options.SubstituteApiVersionInUrl = true;
			});

			services.AddApiVersioning(options =>
			{
				// reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
				options.ReportApiVersions = true;

				// automatically applies an api version based on the name of the defining controller's namespace
				options.Conventions.Add(new VersionByNamespaceConvention());
			});

			services.AddSwaggerConfiguration();

			//Json serializer settings Enum as string
			services.AddMvc(options =>
			{
				options.Filters.Add(typeof(ModelValidationFilter));
				options.EnableEndpointRouting = false;
				options.AllowEmptyInputInBodyModelBinding = true; // ToDo: Need to explore
			})
			.AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
			});

			//Adding Service Clients
			services.AddServicesDependency(Configuration);
		}

		/// <summary>
		/// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		/// </summary>
		/// <param name="app"></param>
		/// <param name="env"></param>
		/// <param name="provider"></param>
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseMiddleware<HttpCodeAndLogMiddleware>();

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			// This line enables the app to use Swagger, with the configuration in the ConfigureServices method.
			app.UseSwagger(options =>
			{
				options.RouteTemplate = "swagger/{documentName}/swagger.json";
			});

			// This line enables Swagger UI, which provides us with a nice, simple UI with which we can view our API calls.
			app.UseSwaggerUI(options =>
			{
				options.RoutePrefix = "swagger";

				// build a swagger endpoint for each discovered API version
				foreach (var description in provider.ApiVersionDescriptions)
				{
					options.SwaggerEndpoint($"{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
				}
			});
		}
	}
}
