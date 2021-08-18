using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdminService.V1.Models.Request
{
	/// <summary>
	/// PersonRequest
	/// </summary>
	public class PersonRequest
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PersonRequest"/> class.
		/// </summary>
		public PersonRequest()
		{
			this.CreatedDate = DateTime.Now;
			this.Banks = new List<BankRequest>();
		}

		[JsonIgnore]
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets AccountNumber.
		/// </summary>
		[JsonProperty(Required = Required.Always)]
		[StringLength(maximumLength: 10, ErrorMessage = "AccountNumber length should not be greater than 10.")]
		public string AccountNumber { get; set; }

		/// <summary>
		/// Gets or sets FirstName.
		/// </summary>
		[JsonProperty(Required = Required.Always)]
		[StringLength(maximumLength: 50, ErrorMessage = "FirstName length should not be greater than 50.")]
		public string FirstName { get; set; }

		/// <summary>
		/// Gets or sets MiddleName.
		/// </summary>
		[StringLength(maximumLength: 50, ErrorMessage = "MiddleName length should not be greater than 50.")]
		public string MiddleName { get; set; }

		/// <summary>
		/// Gets or sets LastName.
		/// </summary>
		[JsonProperty(Required = Required.Always)]
		[StringLength(maximumLength: 50, ErrorMessage = "LastName length should not be greater than 50.")]
		public string LastName { get; set; }

		/// <summary>
		/// Gets or sets DisplayName.
		/// </summary>
		[JsonProperty(Required = Required.Always)]
		[StringLength(maximumLength: 100, ErrorMessage = "LastName length should not be greater than 100.")]
		public string DisplayName { get; set; }

		/// <summary>
		/// Gets or sets Ssn.
		/// </summary>
		[JsonProperty(Required = Required.Always)]
		[StringLength(maximumLength: 12, ErrorMessage = "LastName length should not be greater than 12.")]
		public string Ssn { get; set; }

		/// <summary>
		/// Gets or sets IsActive.
		/// </summary>
		[JsonProperty(Required = Required.Always)]
		public bool? IsActive { get; set; }

		/// <summary>
		/// Gets or sets CreatedBy.
		/// </summary>
		[JsonProperty(Required = Required.Always)]
		[StringLength(maximumLength: 50, ErrorMessage = "LastName length should not be greater than 50.")]
		public string CreatedBy { get; set; }

		/// <summary>
		/// Gets or sets CreatedDate.
		/// </summary>
		[JsonIgnore]
		public DateTime CreatedDate { get; set; }

		/// <summary>
		/// Gets or sets Banks.
		/// </summary>
		[JsonProperty(Required = Required.AllowNull)]
		public List<BankRequest> Banks { get; set; }
	}
}
