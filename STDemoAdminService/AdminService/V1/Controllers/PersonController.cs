using AdminService.V1.Models.Request;
using AdminService.V1.Models.Shared;
using AdminService.V1.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AdminService.V1.Controllers
{
	/// <summary>
	/// PersonController
	/// </summary>
	[Route("api/digitalmaintenance/v{api-version:apiVersion}/[controller]")]
	[ApiController]
	public class PersonController : ControllerBase
	{
		#region Members

		private readonly IConfiguration _confiiguration;
		private readonly IPersonRepository _personRepository;

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="PersonController"/> class.
		/// </summary>
		/// <param name="configuration"></param>
		/// <param name="personRepository"></param>
		public PersonController(IConfiguration configuration, IPersonRepository personRepository)
		{
			_confiiguration = configuration;
			_personRepository = personRepository;
		}

		#endregion

		#region Public Methods"

		/// <summary>
		/// Create a new person record in system.
		/// </summary>
		/// <param name="personRequest"></param>
		/// <returns></returns>
		[HttpPost]
		[ProducesResponseType(type: typeof(int), statusCode: StatusCodes.Status201Created)]
		[ProducesResponseType(type: typeof(ErrorDetails), statusCode: StatusCodes.Status400BadRequest)]
		[ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult> PostBank([FromBody] PersonRequest personRequest)
		{
			int personId = await _personRepository.CreatePerson(personRequest);

			if (personId > 0)
			{
				return new ObjectResult(personId)
				{
					StatusCode = (int)HttpStatusCode.Created
				};
			}
			else
			{
				return new ObjectResult("Unable to process your request. Please contact system admistrator.")
				{
					StatusCode = (int)HttpStatusCode.InternalServerError
				};
			}
		}

		#endregion
	}
}
