using AdminService.V1.Models.Mapper;
using AdminService.V1.Models.Request;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Edm = AdminService.Database.STDemoAdmin.Edm;

namespace AdminService.V1.Repository
{
	/// <summary>
	/// BankRepository
	/// </summary>
	public class PersonRepository : BaseRepository, IPersonRepository
	{
		#region Members
		#endregion

		//CONSTRUCTORS *************************************************************************************************
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="PersonRepository"/> class.
		/// </summary>
		/// <param name="configuration"></param>
		public PersonRepository(IConfiguration configuration) : base(configuration)
		{

		}
		#endregion

		#region Public methods

		/// <summary>
		/// Create a new person record.
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public async Task<int> CreatePerson(PersonRequest request)
		{
			string fullClassName = string.Concat(MethodBase.GetCurrentMethod().ReflectedType.FullName, ".", MethodBase.GetCurrentMethod().Name);
			int dbPersonId = 0;

			try
			{
				if (request == null)
				{
					throw new ArgumentNullException("ModelMapper.CreatePerson - Cannot be Null.");
				}

				using (var dbContext = GetSTDemoAdminContext())
				{
					using (var transaction = dbContext.Database.BeginTransaction())
					{
						if (transaction != null)
						{
							try
							{
								Edm.Person person = request.CreatePerson();
								var dbPerson = await dbContext.AddAsync(person);
								var dbResult = await dbContext.SaveChangesAsync();

								if (dbResult > 0 && dbPerson != null && dbPerson.Entity != null && dbPerson.Entity.Id > 0)
								{
									transaction.Commit();
									dbPersonId = dbPerson.Entity.Id;
								}
								else
								{
									_logger.Error($"{fullClassName} - Unable to commit data. PersonRequest:{JsonConvert.SerializeObject(request)}");
								}
							}
							catch (Exception)
							{
								transaction?.Rollback();
								throw;
							}
						}
					}
				}

				return dbPersonId;
			}
			catch (Exception ex)
			{
				_logger.Error(ex, $"{fullClassName} - ErrorMessage: {ex.Message}");
				throw;
			}
		}

		#endregion
	}
}
