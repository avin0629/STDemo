using AdminService.V1.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminService.V1.Repository
{
	/// <summary>
	/// IBankRepository
	/// </summary>
	public interface IPersonRepository
	{
		/// <summary>
		/// Create a new person record.
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		Task<int> CreatePerson(PersonRequest request);
	}
}
