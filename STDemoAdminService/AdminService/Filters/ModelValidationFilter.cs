using AdminService.V1.Models.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NLog;
using System;
using System.Linq;
using System.Text;

namespace AdminService.Filters
{
	/// <summary>
	/// Model data validation filter.
	/// </summary>
	public class ModelValidationFilter : ActionFilterAttribute
	{
		private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

		public override void OnActionExecuting(ActionExecutingContext context)
		{
			if (!context.ModelState.IsValid)
			{
				ErrorDetails response = this.GetModelValidationErrors(context);
				string validationError = $"DigitalMaintenanceService.Filters.ModelValidationFilter - Received bad request. Url:{context.HttpContext.Request.Path.Value} ModelValidationErrors: {response.Message}";
				this._logger.Error(validationError);
				context.Result = new BadRequestObjectResult(response);
			}
		}

		/// <summary>
		/// Perform model validation State
		/// </summary>
		/// <param name="context"></param>
		/// <returns>ErrorDetails</returns>
		private ErrorDetails GetModelValidationErrors(ActionExecutingContext context)
		{
			StringBuilder errorMessage = new StringBuilder();

			foreach (var state in context.ModelState)
			{
				if (state.Value.ValidationState == ModelValidationState.Invalid)
				{
					if (state.Value.Errors != null && state.Value.Errors.Any())
					{
						errorMessage.Append(string.Join(Environment.NewLine, state.Value.Errors.Select(s => s.Exception?.Message ?? s.ErrorMessage)));
					}
				}
			}

			return new ErrorDetails()
			{
				ErrorCode = "BadRequest",
				Message = errorMessage.ToString()
			};
		}
	}
}
