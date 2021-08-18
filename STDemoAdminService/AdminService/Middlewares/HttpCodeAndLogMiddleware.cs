using Microsoft.AspNetCore.Http;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AdminService.Middlewares
{
	/// <summary>
	/// HttpCodeAndLogMiddleware
	/// </summary>
	public class HttpCodeAndLogMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger _logger;

		/// <summary>
		/// Initializes a new instance of the <see cref="HttpCodeAndLogMiddleware"/> class.
		/// </summary>
		/// <param name="next"></param>
		public HttpCodeAndLogMiddleware(RequestDelegate next)
		{
			_next = next;
			_logger = LogManager.GetCurrentClassLogger();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="httpContext"></param>
		/// <returns></returns>
		public async Task Invoke(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(httpContext, ex);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="httpContext"></param>
		/// <param name="ex"></param>
		/// <returns></returns>
		private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
		{
			HttpStatusCode code;

			if (ex is InvalidOperationException)
			{
				code = HttpStatusCode.NoContent;
				_logger.Info(ex, ex.Message);
			}
			else if ((ex is UnauthorizedAccessException))
			{
				code = HttpStatusCode.Forbidden;
				_logger.Warn(ex, ex.Message);
			}
			else if (ex is ArgumentException
					|| ex is FormatException
					|| ex is System.Xml.Schema.XmlSchemaException
					|| ex is System.Xml.XmlException
					|| ex is System.Xml.XPath.XPathException
					|| ex is System.Xml.Xsl.XsltException)
			{
				code = HttpStatusCode.BadRequest;
				_logger.Info(ex, ex.Message);
			}
			else
			{
				code = HttpStatusCode.InternalServerError;
				_logger.Error(ex, ex.Message);
			}

			httpContext.Response.ContentType = System.Net.Mime.MediaTypeNames.Text.Plain;
			httpContext.Response.StatusCode = (int)code;

			await httpContext.Response.WriteAsync(ex.Message);
		}
	}
}
