using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminService.V1.Models.Shared
{
	/// <summary>
	/// This object describes errors that occur. It is only valid for responses, and ignored in requests.
	/// </summary>
	public class ErrorDetails
	{
		/// <summary>
		/// An error code associated with the error.
		/// </summary>
		/// <value>An error code associated with the error.</value>
		public string ErrorCode
		{
			get;
			set;
		}

		/// <summary>
		/// A short error message.
		/// </summary>
		/// <value>A short error message.</value>
		public string Message
		{
			get;
			set;
		}
	}
}
