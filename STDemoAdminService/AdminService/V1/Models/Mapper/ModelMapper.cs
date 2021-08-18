using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Edm = AdminService.Database.STDemoAdmin.Edm;
using Request = AdminService.V1.Models.Request;

namespace AdminService.V1.Models.Mapper
{
	public static class ModelMapper
	{
		/// <summary>
		/// Person => Map service request model to database entity data model.
		/// </summary>
		/// <param name="source"></param>
		/// <returns></returns>
		public static Edm.Person CreatePerson(this Request.PersonRequest source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("ModelMapper.CreatePerson - Cannot be Null.");
			}

			return new Edm.Person
			{
				AccountNumber = source.AccountNumber,
				CreatedBy = source.CreatedBy,
				CreatedDate = source.CreatedDate,
				DisplayName = source.DisplayName,
				FirstName = source.FirstName,
				IsActive = source.IsActive,
				LastName = source.LastName,
				MiddleName = source.MiddleName,
				Ssn = source.Ssn,
				//Banks = source.Banks;
			};
		}
	}
}
